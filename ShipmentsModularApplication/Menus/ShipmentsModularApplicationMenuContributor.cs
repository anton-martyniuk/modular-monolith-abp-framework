using ShipmentsModularApplication.Permissions;
using ShipmentsModularApplication.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace ShipmentsModularApplication.Menus;

public class ShipmentsModularApplicationMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ShipmentsModularApplicationResource>();
        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ShipmentsModularApplicationMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );


        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ShipmentsModularApplicationMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-chart-line",
                order: 2
            ).RequirePermissions(ShipmentsModularApplicationPermissions.Dashboard.Host)
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;
        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }
}
