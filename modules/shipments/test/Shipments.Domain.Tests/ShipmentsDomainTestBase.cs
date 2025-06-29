using Volo.Abp.Modularity;

namespace Shipments;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class ShipmentsDomainTestBase<TStartupModule> : ShipmentsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
