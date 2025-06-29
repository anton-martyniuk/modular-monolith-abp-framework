using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Shipments;

[DependsOn(
    typeof(ShipmentsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ShipmentsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(ShipmentsApplicationContractsModule).Assembly,
            ShipmentsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ShipmentsHttpApiClientModule>();
        });

    }
}
