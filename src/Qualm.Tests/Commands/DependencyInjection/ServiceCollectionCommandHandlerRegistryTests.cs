using Microsoft.Extensions.DependencyInjection;
using Qualm.Commands.DependencyInjection;
using Xunit;

namespace Qualm.Tests.Commands.DependencyInjection
{
    public class ServiceCollectionCommandHandlerRegistryTests
    {
        [Fact]
        public void RegisterHandler_TestCommand_ServicesRegistered()
        {
            var services = new ServiceCollection();
            var lifetime = ServiceLifetime.Scoped;
            var registry = new ServiceCollectionCommandHandlerRegistry(services, lifetime);

            registry.RegisterHandler(typeof(TestCommand), typeof(TestCommandHandler));

            Assert.Single(services);
        }

        [Fact]
        public void RegisterHandlers_ThisAssembly_GetsCommandHandler()
        {
            var services = new ServiceCollection();
            var registry = new ServiceCollectionCommandHandlerRegistry(services, ServiceLifetime.Scoped);

            registry.RegisterHandlers(typeof(TestCommand).Assembly);

            Assert.Single(services);
        }
    }
}
