using Microsoft.EntityFrameworkCore;
using Shipments.Shipments;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Shipments.EntityFrameworkCore;

public static class ShipmentsDbContextModelCreatingExtensions
{
    public static void ConfigureShipments(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Shipment>(entity =>
        {
            entity.ToTable(ShipmentsDbProperties.DbTablePrefix + "Shipments", ShipmentsDbProperties.DbSchema);
            entity.ConfigureByConvention();
    
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Number);

            entity.Property(x => x.Number).IsRequired();
            entity.Property(x => x.OrderId).IsRequired();
            entity.Property(x => x.Carrier).IsRequired();
            entity.Property(x => x.ReceiverEmail).IsRequired();

            entity.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            entity.OwnsOne(x => x.Address, ownsBuilder =>
            {
                ownsBuilder.Property(x => x.Street).IsRequired();
                ownsBuilder.Property(x => x.City).IsRequired();
                ownsBuilder.Property(x => x.Zip).IsRequired();
            });

            entity.HasMany(x => x.Items)
                .WithOne(x => x.Shipment)
                .HasForeignKey(x => x.ShipmentId);
        });

        builder.Entity<ShipmentItem>(entity =>
        {
            entity.ToTable(ShipmentsDbProperties.DbTablePrefix + "ShipmentItems", ShipmentsDbProperties.DbSchema);
            
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Product).IsRequired();
            entity.Property(x => x.Quantity).IsRequired();
        });

        builder.Entity<Shipment>()
            .Navigation(e => e.Items)
            .AutoInclude();
    }
}
