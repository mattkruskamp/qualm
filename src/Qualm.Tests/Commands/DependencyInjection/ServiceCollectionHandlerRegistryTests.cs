using Microsoft.Extensions.DependencyInjection;
using Qualm.Commands.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Qualm.Tests.Commands.DependencyInjection
{
    public class ServiceCollectionHandlerRegistryTests
    {
        [Fact]
        public void RegisterHandler_TestCommand_ServicesRegistered()
        {
            var services = new ServiceCollection();
            var lifetime = ServiceLifetime.Scoped;
            var registry = new ServiceCollectionHandlerRegistry(services, lifetime);

            registry.RegisterHandler(typeof(TestCommand), typeof(TestCommandHandler));

            Assert.Single(services);
        }

        [Fact]
        public void RegisterHandlers_ThisAssembly_GetsCommandHandler()
        {
            var services = new ServiceCollection();
            var registry = new ServiceCollectionHandlerRegistry(services, ServiceLifetime.Scoped);

            registry.RegisterHandlers(typeof(TestCommand).Assembly);

            Assert.Single(services);
        }
    }
}
