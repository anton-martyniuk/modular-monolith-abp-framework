using System;

namespace Stocks.Models;

public class StockDto
{
    public string ProductName { get; set; } = null!;
    public int AvailableQuantity { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
