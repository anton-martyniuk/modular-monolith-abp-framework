using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Shipments.Web.Menus;

public class ShipmentsMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(ShipmentsMenus.Prefix, displayName: "Shipments", "~/Shipments", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
