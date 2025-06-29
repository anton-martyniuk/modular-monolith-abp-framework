using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShipmentsModularApplication.Data;

public class ShipmentsModularApplicationDbContextFactory : IDesignTimeDbContextFactory<ShipmentsModularApplicationDbContext>
{
    public ShipmentsModularApplicationDbContext CreateDbContext(string[] args)
    {
        ShipmentsModularApplicationEfCoreEntityExtensionMappings.Configure();
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ShipmentsModularApplicationDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new ShipmentsModularApplicationDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}