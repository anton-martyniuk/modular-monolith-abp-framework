using System;
using Volo.Abp.Domain.Entities;

namespace Stocks.Entities;

public class ProductStock : Entity<Guid>
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int AvailableQuantity { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}