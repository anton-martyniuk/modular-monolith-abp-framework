using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stocks.Models;
using Volo.Abp.DependencyInjection;

namespace Stocks.Integration;

public interface IStockIntegrationService : ITransientDependency
{
    Task<CheckStockResponse> CheckStockAsync(
        CheckStockRequest request, 
        CancellationToken cancellationToken = default);

    Task<List<StockItemDto>> GetAllStocksAsync(
        CancellationToken cancellationToken = default);
}
