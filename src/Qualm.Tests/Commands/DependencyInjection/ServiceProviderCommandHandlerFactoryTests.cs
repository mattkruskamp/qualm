using Microsoft.Extensions.DependencyInjection;
using Qualm.Commands.DependencyInjection;
using System;
using Xunit;

namespace Qualm.Tests.Commands.DependencyInjection
{
    public class ServiceProviderCommandHandlerFactoryTests
    {
        private ServiceProviderCommandHandlerFactory BuildHandlerFactory()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<TestCommandHandler>();
            var provider = collection.BuildServiceProvider();
            return new ServiceProviderCommandHandlerFactory(provider);
        }
        
        [Fact]
        public void Create_NullHandler_ThrowsException()
        {
            var factory = BuildHandlerFactory();

            Assert.Throws<InvalidOperationException>(() => factory.Create(null));
        }

        [Fact]
        public void Create_NonCommandHandlerType_ThrowsException()
        {
            var factory = BuildHandlerFactory();

            Assert.Throws<InvalidOperationException>(() => factory.Create(typeof(string)));
        }

        [Fact]
        public void Create_GoodHandler_ReturnsHandler()
        {
            var factory = BuildHandlerFactory();

            var result = factory.Create(typeof(TestCommandHandler));

            Assert.NotNull(result);
        }
    }
}
