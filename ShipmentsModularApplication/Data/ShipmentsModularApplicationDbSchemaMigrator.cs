using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ShipmentsModularApplication.Data;

public class ShipmentsModularApplicationDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public ShipmentsModularApplicationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the ShipmentsModularApplicationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ShipmentsModularApplicationDbContext>()
            .Database
            .MigrateAsync();

    }
}
