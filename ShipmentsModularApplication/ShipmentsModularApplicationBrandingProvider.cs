using Microsoft.Extensions.Localization;
using ShipmentsModularApplication.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ShipmentsModularApplication;

[Dependency(ReplaceServices = true)]
public class ShipmentsModularApplicationBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ShipmentsModularApplicationResource> _localizer;

    public ShipmentsModularApplicationBrandingProvider(IStringLocalizer<ShipmentsModularApplicationResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}