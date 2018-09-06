using Microsoft.Extensions.DependencyInjection;
using Qualm.Commands.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Qualm.Tests.Commands.DependencyInjection
{
    public class ServiceProviderHandlerFactoryTests
    {
        private ServiceProviderHandlerFactory BuildHandlerFactory()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<TestCommandHandler>();
            var provider = collection.BuildServiceProvider();
            return new ServiceProviderHandlerFactory(provider);
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
