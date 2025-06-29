using Stocks.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Stocks.Permissions;

public class StocksPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(StocksPermissions.GroupName, L("Permission:Stocks"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StocksResource>(name);
    }
}
