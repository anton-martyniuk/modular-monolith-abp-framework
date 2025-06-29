using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shipments.Events;
using Stocks.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace Stocks.EventHandlers;

public class ShipmentCreatedEventHandler : IDistributedEventHandler<ShipmentCreatedEvent>, ITransientDependency
{
    private readonly IRepository<ProductStock, Guid> _stockRepository;
    private readonly ILogger<ShipmentCreatedEventHandler> _logger;

    public ShipmentCreatedEventHandler(IRepository<ProductStock, Guid> stockRepository, ILogger<ShipmentCreatedEventHandler> logger)
    {
        _stockRepository = stockRepository;
        _logger = logger;
    }

    public async Task HandleEventAsync(ShipmentCreatedEvent eventData)
    {
        _logger.LogInformation("Updating stock for {Count} products. Operation: {Operation}", eventData.Shipments.Count, eventData);
        
        var productNames = eventData.Shipments.Select(x => x.Product).Distinct().ToList();
        var stocks = await _stockRepository.GetListAsync(x => productNames.Contains(x.ProductName));

        var stocksDictionary = stocks.ToDictionary(x => x.ProductName, x => x);
        
        foreach (var product in eventData.Shipments)
        {
            var stock = stocksDictionary[product.Product];
            var newQuantity = stock.AvailableQuantity - product.Quantity;

            if (newQuantity < 0)
            {
                var errorMessage = $"Insufficient stock for product {product.Product}. " +
                                 $"Available: {stock.AvailableQuantity}, " +
                                 $"Requested change: {product.Quantity}";

                _logger.LogWarning(errorMessage);
                return;
            }

            stock.AvailableQuantity = newQuantity;
            stock.LastUpdatedAt = DateTime.UtcNow;

            _logger.LogInformation(
                "Updated stock for product {ProductId}. Old quantity: {OldQuantity}, New quantity: {NewQuantity}", 
                product.Product,
                stock.AvailableQuantity,
                newQuantity);
        }

        await _stockRepository.UpdateManyAsync(stocks);

        _logger.LogInformation("Stock update completed successfully");
    }
}