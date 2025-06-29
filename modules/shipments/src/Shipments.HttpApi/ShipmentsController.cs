using Shipments.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Shipments;

public abstract class ShipmentsController : AbpControllerBase
{
    protected ShipmentsController()
    {
        LocalizationResource = typeof(ShipmentsResource);
    }
}
