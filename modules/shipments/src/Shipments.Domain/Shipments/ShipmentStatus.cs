namespace Shipments.Shipments;

public enum ShipmentStatus
{
    Created,
    Processing,
    Dispatched,
    InTransit,
    WaitingCustomer,
    Delivered,
    Cancelled
}