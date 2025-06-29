using Stocks.Localization;
using Volo.Abp.Application.Services;

namespace Stocks;

public abstract class StocksAppService : ApplicationService
{
    protected StocksAppService()
    {
        LocalizationResource = typeof(StocksResource);
        ObjectMapperContext = typeof(StocksModule);
    }
}
