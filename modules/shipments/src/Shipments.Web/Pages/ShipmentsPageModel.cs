using Shipments.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Shipments.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ShipmentsPageModel : AbpPageModel
{
    protected ShipmentsPageModel()
    {
        LocalizationResourceType = typeof(ShipmentsResource);
        ObjectMapperContext = typeof(ShipmentsWebModule);
    }
}
