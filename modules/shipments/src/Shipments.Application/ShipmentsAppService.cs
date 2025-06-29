using Shipments.Localization;
using Volo.Abp.Application.Services;

namespace Shipments;

public abstract class ShipmentsAppService : ApplicationService
{
    protected ShipmentsAppService()
    {
        LocalizationResource = typeof(ShipmentsResource);
        ObjectMapperContext = typeof(ShipmentsApplicationModule);
    }
}
