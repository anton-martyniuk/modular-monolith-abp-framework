namespace Stocks.Models;

public record CheckStockResponse(bool IsSuccess, string? ErrorMessage = null);
