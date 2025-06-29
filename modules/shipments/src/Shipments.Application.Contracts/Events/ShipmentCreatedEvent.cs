using System.Collections.Generic;
using Shipments.Shipments.Models;

namespace Shipments.Events;

public record ShipmentCreatedEvent(List<ShipmentItemResponse> Shipments);