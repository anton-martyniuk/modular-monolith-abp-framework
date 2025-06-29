namespace Stocks;

public static class StocksDbProperties
{
    public static string DbTablePrefix { get; set; } = "Stocks";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Stocks";
}
