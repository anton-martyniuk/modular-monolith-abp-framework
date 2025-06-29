using Volo.Abp.Modularity;

namespace Shipments;

[DependsOn(
    typeof(ShipmentsApplicationModule),
    typeof(ShipmentsDomainTestModule)
    )]
public class ShipmentsApplicationTestModule : AbpModule
{

}
