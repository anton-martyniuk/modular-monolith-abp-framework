using Microsoft.EntityFrameworkCore;
using Shipments.Shipments;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Shipments.EntityFrameworkCore;

[ConnectionStringName(ShipmentsDbProperties.ConnectionStringName)]
public class ShipmentsDbContext : AbpDbContext<ShipmentsDbContext>, IShipmentsDbContext
{
    public DbSet<Shipment> Shipments { get; }

    public ShipmentsDbContext(DbContextOptions<ShipmentsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureShipments();
    }
}
