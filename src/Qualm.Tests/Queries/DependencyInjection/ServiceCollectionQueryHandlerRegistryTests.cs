using Microsoft.Extensions.DependencyInjection;
using Qualm.Queries.DependencyInjection;
using Xunit;

namespace Qualm.Tests.Queries.DependencyInjection
{
    public class ServiceCollectionQueryHandlerRegistryTests
    {
        [Fact]
        public void RegisterHandler_TestQuery_ServicesRegistered()
        {
            var services = new ServiceCollection();
            var lifetime = ServiceLifetime.Scoped;
            var registry = new ServiceCollectionQueryHandlerRegistry(services, lifetime);

            registry.RegisterHandler(typeof(TestQuery), typeof(TestQueryHandler));

            Assert.Single(services);
        }

        [Fact]
        public void RegisterHandlers_ThisAssembly_GetsCommandHandler()
        {
            var services = new ServiceCollection();
            var registry = new ServiceCollectionQueryHandlerRegistry(services, ServiceLifetime.Scoped);

            registry.RegisterHandlers(typeof(TestQuery).Assembly);

            Assert.Single(services);
        }
    }
}
