using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Shipments;

[DependsOn(
    typeof(ShipmentsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ShipmentsApplicationContractsModule : AbpModule
{

}
