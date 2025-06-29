using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Shipments.EntityFrameworkCore;

[DependsOn(
    typeof(ShipmentsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ShipmentsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ShipmentsDbContext>(options =>
        {
            options.AddDefaultRepositories<IShipmentsDbContext>(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
