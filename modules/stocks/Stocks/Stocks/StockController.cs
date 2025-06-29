using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stocks.Models;
using Volo.Abp.AspNetCore.Mvc;

namespace Stocks.Stocks;

[ApiController]
[Route("api/stocks")]
public class StockController : AbpControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<List<StockDto>> GetAllStocksAsync()
    {
        return await _stockService.GetAllStocksAsync();
    }
    
    [HttpPost]
    public async Task<IActionResult> SeedStocksAsync()
    {
        await _stockService.SeedStocksAsync();
        return Ok();
    }
}
