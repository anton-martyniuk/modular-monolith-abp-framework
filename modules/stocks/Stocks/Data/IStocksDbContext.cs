using Microsoft.EntityFrameworkCore;
using Stocks.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stocks.Data;

[ConnectionStringName(StocksDbProperties.ConnectionStringName)]
public interface IStocksDbContext : IEfCoreDbContext
{
    DbSet<ProductStock> ProductStocks { get; }
}
