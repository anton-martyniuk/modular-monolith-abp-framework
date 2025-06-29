using ShipmentsModularApplication.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ShipmentsModularApplication.Permissions;

public class ShipmentsModularApplicationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ShipmentsModularApplicationPermissions.GroupName);


        myGroup.AddPermission(ShipmentsModularApplicationPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);

        
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ShipmentsModularApplicationPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ShipmentsModularApplicationResource>(name);
    }
}
