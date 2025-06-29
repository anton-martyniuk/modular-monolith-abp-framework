using System.Collections.Generic;

namespace Shipments.Shipments.Models;

public sealed record ShipmentResponse(
    string Number,
    string OrderId,
    Address Address,
    string Carrier,
    string ReceiverEmail,
    ShipmentStatus Status,
    List<ShipmentItemResponse> Items);
    
public sealed record ShipmentItemResponse(string Product, int Quantity);