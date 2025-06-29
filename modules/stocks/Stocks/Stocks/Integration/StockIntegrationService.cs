using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Stocks.Integration;
using Stocks.Models;
using Volo.Abp.Domain.Repositories;
using ProductStock = Stocks.Entities.ProductStock;

namespace Stocks.Stocks.Integration;

public class StockIntegrationService : IStockIntegrationService
{
    private readonly IRepository<ProductStock, Guid> _stockRepository;
    private readonly ILogger<StockIntegrationService> _logger;

    public StockIntegrationService(
        IRepository<ProductStock, Guid> stockRepository,
        ILogger<StockIntegrationService> logger)
    {
        _stockRepository = stockRepository;
        _logger = logger;
    }

    public async Task<CheckStockResponse> CheckStockAsync(
        CheckStockRequest request, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking stock for {Count} products", request.Products.Count);

        var productIds = request.Products.Select(x => x.ProductId).ToList();

        var stocks = await _stockRepository.GetListAsync(x => productIds.Contains(x.ProductName), cancellationToken: cancellationToken);

        var stocksDictionary = stocks.ToDictionary(x => x.ProductName, x => x.AvailableQuantity);

        foreach (var product in request.Products)
        {
            if (!stocksDictionary.TryGetValue(product.ProductId, out var availableQuantity))
            {
                _logger.LogWarning("Product {ProductId} not found in stock", product.ProductId);
                return new CheckStockResponse(false, $"Product {product.ProductId} not found in stock");
            }

            if (availableQuantity >= product.Quantity)
            {
                continue;
            }

            _logger.LogWarning(
                "Insufficient stock for product {ProductId}. Required: {Required}, Available: {Available}", 
                product.ProductId, product.Quantity, availableQuantity);

            return new CheckStockResponse(
                false, 
                $"Insufficient stock for product {product.ProductId}. Required: {product.Quantity}, Available: {availableQuantity}");
        }

        _logger.LogInformation("Stock check passed for all products");

        return new CheckStockResponse(true);
    }

    public async Task<List<StockItemDto>> GetAllStocksAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all available stocks");

        var stocks = await _stockRepository.GetListAsync(
            x => x.AvailableQuantity > 0,
            cancellationToken: cancellationToken);

        _logger.LogInformation("Found {Count} products with available stock", stocks.Count);

        return stocks.Select(stock => new StockItemDto
        {
            Id = stock.Id,
            ProductName = stock.ProductName,
            AvailableQuantity = stock.AvailableQuantity,
            LastUpdatedAt = stock.LastUpdatedAt
        }).ToList();
    }
}
