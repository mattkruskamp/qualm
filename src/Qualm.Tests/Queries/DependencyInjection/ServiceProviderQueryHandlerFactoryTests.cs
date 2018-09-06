using Microsoft.Extensions.DependencyInjection;
using Qualm.Queries.DependencyInjection;
using System;
using Xunit;

namespace Qualm.Tests.Queries.DependencyInjection
{
    public class ServiceProviderQueryHandlerFactoryTests
    {
        private ServiceProviderQueryHandlerFactory BuildHandlerFactory()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<TestQueryHandler>();
            var provider = collection.BuildServiceProvider();
            return new ServiceProviderQueryHandlerFactory(provider);
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

            var result = factory.Create(typeof(TestQueryHandler));

            Assert.NotNull(result);
        }
    }
}
