using Microsoft.EntityFrameworkCore;
using Stocks.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stocks.Data;

[ConnectionStringName(StocksDbProperties.ConnectionStringName)]
public class StocksDbContext : AbpDbContext<StocksDbContext>, IStocksDbContext
{
    public DbSet<ProductStock> ProductStocks { get; }

    public StocksDbContext(DbContextOptions<StocksDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureStocks();
    }
}
