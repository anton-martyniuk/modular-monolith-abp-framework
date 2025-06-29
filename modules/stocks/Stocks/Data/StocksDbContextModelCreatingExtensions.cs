using Microsoft.EntityFrameworkCore;
using Stocks.Entities;
using Volo.Abp;

namespace Stocks.Data;

public static class StocksDbContextModelCreatingExtensions
{
    public static void ConfigureStocks(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ProductStock>(entity =>
        {
            entity.ToTable("ProductStocks", StocksDbProperties.DbSchema);
    
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.ProductName).IsUnique();
    
            entity.Property(x => x.ProductName).IsRequired();
            entity.Property(x => x.AvailableQuantity).IsRequired();
            entity.Property(x => x.LastUpdatedAt).IsRequired();
        });
    }
}
