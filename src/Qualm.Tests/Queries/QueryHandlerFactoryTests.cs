using Qualm.Queries;
using System;
using Xunit;

namespace Qualm.Tests.Queries
{
    public class QueryHandlerFactoryTests
    {
        private QueryHandlerFactory BuildQueryHandlerFactory()
        {
            return new QueryHandlerFactory();
        }

        [Fact]
        public void Create_NullHandler_ThrowsException()
        {
            var factory = BuildQueryHandlerFactory();

            Assert.Throws<InvalidOperationException>(() => factory.Create(null));
        }

        [Fact]
        public void Create_NonQueryHandlerType_ThrowsException()
        {
            var factory = BuildQueryHandlerFactory();

            Assert.Throws<InvalidOperationException>(() =>
                factory.Create(typeof(string)));
        }

        [Fact]
        public void Create_QueryHandler_NotNull()
        {
            var factory = BuildQueryHandlerFactory();

            var handler = factory.Create(typeof(TestQueryHandler));

            Assert.NotNull(handler);
        }
    }
}
