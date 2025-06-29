using Volo.Abp.Reflection;

namespace Stocks.Permissions;

public class StocksPermissions
{
    public const string GroupName = "Stocks";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(StocksPermissions));
    }
}
