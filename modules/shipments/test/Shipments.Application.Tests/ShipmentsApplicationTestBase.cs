using Volo.Abp.Modularity;

namespace Shipments;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class ShipmentsApplicationTestBase<TStartupModule> : ShipmentsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
