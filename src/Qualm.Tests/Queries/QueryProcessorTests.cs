using Moq;
using Qualm.Queries;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Qualm.Tests.Queries
{
    public class QueryProcessorTests
    {
        private QueryProcessor BuildQueryProcessor()
        {
            var registry = new Mock<IQueryHandlerRegistry>();
            registry.Setup(m => m.GetHandler(It.IsAny<Type>()))
                .Returns(typeof(TestQueryHandler));

            var factory = new Mock<IQueryHandlerFactory>();
            factory.Setup(m => m.Create(It.IsAny<Type>()))
                .Returns(new TestQueryHandler());

            return new QueryProcessor(registry.Object, factory.Object);
        }

        [Fact]
        public async Task ExecuteAsync_NullQuery_ThrowsException()
        {
            var processor = BuildQueryProcessor();

            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await processor.ExecuteAsync<IQuery>(null));
        }

        [Fact]
        public async Task ExecuteAsync_Command_ChangesCommand()
        {
            var processor = BuildQueryProcessor();
            var command = new TestQuery();

            var result = await processor.ExecuteAsync(command);

            Assert.NotNull(result);
        }
    }
}
