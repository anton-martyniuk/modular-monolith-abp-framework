using Localization.Resources.AbpUi;
using Shipments.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Shipments;

[DependsOn(
    typeof(ShipmentsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class ShipmentsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ShipmentsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ShipmentsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
