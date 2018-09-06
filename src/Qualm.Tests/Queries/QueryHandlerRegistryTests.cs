using Qualm.Queries;
using System;
using Xunit;

namespace Qualm.Tests.Queries
{
    public class QueryHandlerRegistryTests
    {
        private QueryHandlerRegistry BuildQueryHandlerRegistry()
        {
            return new QueryHandlerRegistry();
        }

        [Fact]
        public void Register_NullCommandOrType_ThrowsException()
        {
            var registry = BuildQueryHandlerRegistry();

            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(null, typeof(TestQueryHandler)));
            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(typeof(TestQuery), null));
        }

        [Fact]
        public void Register_NotQueryOrHandlerType_ThrowsException()
        {
            var registry = BuildQueryHandlerRegistry();

            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(typeof(string), typeof(TestQueryHandler)));
            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(typeof(TestQuery), typeof(string)));
        }

        [Fact]
        public void RegisterAndGetQuery_ValidQuery_ReturnsTestQueryHandler()
        {
            var registry = BuildQueryHandlerRegistry();

            registry.RegisterHandler(typeof(TestQuery), typeof(TestQueryHandler));
            var type = registry.GetHandler(typeof(TestQuery));

            Assert.Equal(typeof(TestQueryHandler), type);
        }

        [Fact]
        public void Get_NoQuery_ReturnsNull()
        {
            var registry = BuildQueryHandlerRegistry();

            var type = registry.GetHandler(typeof(string));

            Assert.Null(type);
        }

        [Fact]
        public void RegisterHandlers_ThisAssembly_GetsQueryHandler()
        {
            var registry = BuildQueryHandlerRegistry();

            registry.RegisterHandlers(typeof(TestQuery).Assembly);

            var type = registry.GetHandler(typeof(TestQuery));
            Assert.Equal(typeof(TestQueryHandler), type);
        }
    }
}
