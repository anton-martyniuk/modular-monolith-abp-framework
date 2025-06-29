using Shipments.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Shipments.Permissions;

public class ShipmentsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ShipmentsPermissions.GroupName, L("Permission:Shipments"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ShipmentsResource>(name);
    }
}
