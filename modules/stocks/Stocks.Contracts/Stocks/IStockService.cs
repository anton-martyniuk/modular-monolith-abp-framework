using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stocks.Models;
using Volo.Abp.Application.Services;

namespace Stocks.Stocks;

public interface IStockService : IApplicationService
{
    Task<List<StockDto>> GetAllStocksAsync(CancellationToken cancellationToken = default);

    Task SeedStocksAsync();
}
