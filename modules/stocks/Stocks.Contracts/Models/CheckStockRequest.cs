using System.Collections.Generic;

namespace Stocks.Models;

public record CheckStockRequest(List<ProductStock> Products);
