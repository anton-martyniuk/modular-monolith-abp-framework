using System;

namespace Shipments.Shipments;

public class ShipmentItem
{
    public long Id { get; set; }

    public required string Product { get; set; }

    public required int Quantity { get; set; }

    public Guid ShipmentId { get; set; }

    public Shipment Shipment { get; set; } = null!;
}