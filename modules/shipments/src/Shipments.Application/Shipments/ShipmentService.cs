using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shipments.Events;
using Shipments.Shipments.Models;
using Stocks.Integration;
using Stocks.Models;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace Shipments.Shipments;

public class ShipmentService : ShipmentsAppService, IShipmentService
{
    private readonly IRepository<Shipment, Guid> _shipmentRepository;
    private readonly IStockIntegrationService _stockIntegrationService;
    private readonly IDistributedEventBus _eventBus;

    public ShipmentService(IRepository<Shipment, Guid> shipmentRepository, IStockIntegrationService stockIntegrationService, IDistributedEventBus eventBus)
    {
        _shipmentRepository = shipmentRepository;
        _stockIntegrationService = stockIntegrationService;
        _eventBus = eventBus;
    }

    public async Task<ShipmentResponse> CreateShipmentAsync(CreateShipmentRequest request, CancellationToken cancellationToken = default)
    {
        var shipmentExists = await _shipmentRepository.AnyAsync(x => x.OrderId == request.OrderId, cancellationToken: cancellationToken);
        if (shipmentExists)
        {
            Logger.LogInformation("Shipment for order '{OrderId}' already exists", request.OrderId);
            throw new UserFriendlyException($"Shipment for order '{request.OrderId}' already exists");
        }
        
        var stockRequest = CreateStockRequest(request);

        var stockResponse = await _stockIntegrationService.CheckStockAsync(stockRequest, cancellationToken);
        if (!stockResponse.IsSuccess)
        {
            Logger.LogInformation("Stock check failed: {ErrorMessage}", stockResponse.ErrorMessage);
            throw new UserFriendlyException(stockResponse.ErrorMessage ?? "Products not available in stock");
        }

        var shipment = ObjectMapper.Map<CreateShipmentRequest, Shipment>(request);

        await _shipmentRepository.InsertAsync(shipment, cancellationToken: cancellationToken);
        
        var shipmentCreatedEvent = CreateEvent(shipment);
        await _eventBus.PublishAsync(shipmentCreatedEvent);

        Logger.LogInformation("Created shipment: {@Shipment}", shipment);

        return ObjectMapper.Map<Shipment, ShipmentResponse>(shipment);
    }

    public async Task<ShipmentResponse?> GetShipmentByNumberAsync(string shipmentNumber, CancellationToken cancellationToken = default)
    {
        var shipment = await _shipmentRepository.FirstOrDefaultAsync(x => x.Number == shipmentNumber, cancellationToken: cancellationToken);

        if (shipment is null)
        {
            Logger.LogDebug("Shipment with number {ShipmentNumber} not found", shipmentNumber);
            return null;
        }

        var response = ObjectMapper.Map<Shipment, ShipmentResponse>(shipment);
        return response;
    }

    public async Task UpdateShipmentStatusAsync(string shipmentNumber, UpdateShipmentStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var shipment = await _shipmentRepository.FirstOrDefaultAsync(x => x.Number == shipmentNumber, cancellationToken: cancellationToken);

        if (shipment is null)
        {
            Logger.LogDebug("Shipment with number {ShipmentNumber} not found", shipmentNumber);
            throw new UserFriendlyException($"Shipment with number '{shipmentNumber}' not found");
        }

        shipment.Status = request.Status;
        shipment.UpdatedAt = DateTime.UtcNow;

        await _shipmentRepository.UpdateAsync(shipment, cancellationToken: cancellationToken);

        Logger.LogInformation("Updated state of shipment {ShipmentNumber} to {NewState}", shipmentNumber, request.Status);
    }

    public async Task<List<ShipmentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var shipments = await _shipmentRepository.GetListAsync(cancellationToken: cancellationToken);

        return shipments
            .Select(shipment => ObjectMapper.Map<Shipment, ShipmentResponse>(shipment))
            .ToList();
    }
    
    private static CheckStockRequest CreateStockRequest(CreateShipmentRequest request)
    {
        return new CheckStockRequest(
            request.Items
                .Select(x => new ProductStock(x.Product, x.Quantity))
                .ToList()
        );
    }
    
    private static ShipmentCreatedEvent CreateEvent(Shipment shipment)
    {
        var items = shipment.Items
            .Select(x => new ShipmentItemResponse(x.Product, x.Quantity))
            .ToList();
    
        return new ShipmentCreatedEvent(items);
    }
}