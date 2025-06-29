using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Stocks.UI.Menus;

public class StocksMenuContributor : IMenuContributor
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
        context.Menu.AddItem(new ApplicationMenuItem(StocksMenus.Prefix, displayName: "Stocks", "~/Stocks", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
