using System.Collections.Generic;

namespace Shipments.Shipments.Models;

public sealed record CreateShipmentRequest(
    string Number,
    string OrderId,
    Address Address,
    string Carrier,
    string ReceiverEmail,
    List<ShipmentItemRequest> Items);