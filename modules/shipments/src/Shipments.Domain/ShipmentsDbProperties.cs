namespace Shipments;

public static class ShipmentsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Shipments";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Shipments";
}
