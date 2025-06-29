using Stocks.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Stocks.UI.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class StocksPageModel : AbpPageModel
{
    protected StocksPageModel()
    {
        LocalizationResourceType = typeof(StocksResource);
        ObjectMapperContext = typeof(StocksWebModule);
    }
}
