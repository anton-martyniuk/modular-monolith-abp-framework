using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Shipments;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class ShipmentsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ShipmentsInstallerModule>();
        });
    }
}
