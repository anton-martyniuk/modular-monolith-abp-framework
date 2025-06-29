using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Shipments.Shipments;
using Stocks.Models;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ProductStock = Stocks.Entities.ProductStock;

namespace Stocks.Stocks;

public class StockService : ApplicationService, IStockService
{
    private readonly IRepository<ProductStock, Guid> _stockRepository;
    
    private readonly string[] _carrierNames = ["DHL", "FedEx", "UPS", "USPS"];

    public StockService(IRepository<ProductStock, Guid> stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<List<StockDto>> GetAllStocksAsync(CancellationToken cancellationToken = default)
    {
        var stocks = await _stockRepository.GetListAsync(cancellationToken: cancellationToken);

        return stocks.Select(stock => new StockDto
        {
            ProductName = stock.ProductName,
            AvailableQuantity = stock.AvailableQuantity,
            LastUpdatedAt = stock.LastUpdatedAt
        }).ToList();
    }

    public async Task SeedStocksAsync()
    {
        if (await _stockRepository.CountAsync(_ => true) > 0)
        {
            return;
        }

        var shipmentItems = await GenerateShipmentsAsync();
        await SaveStocksAsync(shipmentItems);
    }
    
    private async Task<List<ShipmentItem>> GenerateShipmentsAsync()
    {
        var fakeShipments = new Faker<Shipment>()
            .RuleFor(s => s.Id, _ => Guid.NewGuid())
            .RuleFor(s => s.Number, f => f.Commerce.Ean8())
            .RuleFor(s => s.OrderId, f => f.Commerce.Ean13())
            .RuleFor(s => s.Address, f => new Address
            {
                Street = f.Address.StreetAddress(),
                City = f.Address.City(),
                Zip = f.Address.ZipCode()
            })
            .RuleFor(s => s.Carrier, f => f.PickRandom(_carrierNames))
            .RuleFor(s => s.ReceiverEmail, f => f.Internet.Email())
            .RuleFor(s => s.Items, f =>
            {
                var itemCount = f.Random.Int(1, 5);
                return Enumerable.Range(1, itemCount)
                    .Select(_ => new ShipmentItem
                    {
                        Product = f.PickRandom(f.Commerce.ProductName()),
                        Quantity = f.Random.Int(1, 5)
                    })
                    .ToList();
            })
            .RuleFor(s => s.Status, f => f.PickRandom<ShipmentStatus>())
            .RuleFor(s => s.CreatedAt, f => f.Date.Past(1).ToUniversalTime())
            .RuleFor(s => s.UpdatedAt, (f, s) => 
                s.Status == ShipmentStatus.Created 
                    ? null 
                    : f.Date.Between(s.CreatedAt, DateTime.UtcNow).ToUniversalTime());

        var shipments = fakeShipments.Generate(10);
        
        return shipments
            .SelectMany(x => x.Items)
            .DistinctBy(x => x.Product)
            .ToList();
    }
    
    private async Task SaveStocksAsync(List<ShipmentItem> shipmentItems)
    {
        var faker = new Faker();
        var stocks = shipmentItems.Select(x =>
        {
            var quantity = faker.Random.Bool(0.15f) ? 0 : faker.Random.Int(1, 100);
            
            return new ProductStock
            {
                Id = Guid.NewGuid(),
                ProductName = x.Product,
                AvailableQuantity = quantity,
                LastUpdatedAt = faker.Date.Past().ToUniversalTime()
            };
        }).ToList();

        await _stockRepository.InsertManyAsync(stocks);
    }
}
