using System;

namespace Stocks.Models;

public class StockItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int AvailableQuantity { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
