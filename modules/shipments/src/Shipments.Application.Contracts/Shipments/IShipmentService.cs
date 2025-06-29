using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shipments.Shipments.Models;
using Volo.Abp.Application.Services;

namespace Shipments.Shipments;

public interface IShipmentService : IApplicationService
{
    Task<ShipmentResponse> CreateShipmentAsync(CreateShipmentRequest request, CancellationToken cancellationToken = default);

    Task<ShipmentResponse?> GetShipmentByNumberAsync(string shipmentNumber, CancellationToken cancellationToken = default);

    Task UpdateShipmentStatusAsync(string shipmentNumber, UpdateShipmentStatusRequest request, CancellationToken cancellationToken = default);

    Task<List<ShipmentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
}