using Microsoft.EntityFrameworkCore;
using Shipments.Shipments;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Shipments.EntityFrameworkCore;

[ConnectionStringName(ShipmentsDbProperties.ConnectionStringName)]
public interface IShipmentsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    
    DbSet<Shipment> Shipments { get; }
}
