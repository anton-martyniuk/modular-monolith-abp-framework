using Stocks.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Stocks.csproj"); 
await builder.RunAbpModuleAsync<StocksTestsModule>(applicationName: "Stocks");

public partial class TestProgram
{
}
