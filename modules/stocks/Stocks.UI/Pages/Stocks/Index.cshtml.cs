using System.Collections.Generic;
using System.Threading.Tasks;
using Stocks.Models;
using Stocks.Stocks;

namespace Stocks.UI.Pages.Stocks;

public class IndexModel : StocksPageModel
{
    private readonly IStockService _stockService;

    public List<StockDto> Stocks { get; set; } = new();

    public IndexModel(IStockService stockService)
    {
        _stockService = stockService;
    }

    public async Task OnGetAsync()
    {
        Stocks = await _stockService.GetAllStocksAsync();
    }
}
