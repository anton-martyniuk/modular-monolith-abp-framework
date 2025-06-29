using Volo.Abp.Modularity;

namespace Shipments;

[DependsOn(
    typeof(ShipmentsDomainModule),
    typeof(ShipmentsTestBaseModule)
)]
public class ShipmentsDomainTestModule : AbpModule
{

}
