using Volo.Abp.Application.Services;
using ShipmentsModularApplication.Localization;

namespace ShipmentsModularApplication.Services;

/* Inherit your application services from this class. */
public abstract class ShipmentsModularApplicationAppService : ApplicationService
{
    protected ShipmentsModularApplicationAppService()
    {
        LocalizationResource = typeof(ShipmentsModularApplicationResource);
    }
}