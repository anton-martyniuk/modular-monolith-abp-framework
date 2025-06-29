using Stocks.Data;
using Microsoft.EntityFrameworkCore;
using Shipments.EntityFrameworkCore;
using Shipments.Shipments;
using Stocks.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace ShipmentsModularApplication.Data;

[ReplaceDbContext(typeof(IShipmentsDbContext))]
[ReplaceDbContext(typeof(IStocksDbContext))]
public class ShipmentsModularApplicationDbContext :
    AbpDbContext<ShipmentsModularApplicationDbContext>,
    IShipmentsDbContext,
    IStocksDbContext
{
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;
    
    public DbSet<Shipment> Shipments { get; }
    
    public DbSet<ProductStock> ProductStocks { get; }

    public ShipmentsModularApplicationDbContext(DbContextOptions<ShipmentsModularApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureShipments();
        builder.ConfigureStocks();

        /* Include modules to your migration db context */

        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        
        /* Configure your own entities here */
    }
}

