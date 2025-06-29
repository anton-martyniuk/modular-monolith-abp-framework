using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Commercial.SuiteTemplates;

namespace Shipments;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(VoloAbpCommercialSuiteTemplatesModule),
    typeof(ShipmentsDomainSharedModule)
)]
public class ShipmentsDomainModule : AbpModule
{

}
