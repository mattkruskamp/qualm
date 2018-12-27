using Moq;
using Qualm.Queuing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Qualm.Tests.Queuing
{
    public class QueueProcessorTests
    {
        private QueueProcessor BuildQueueProcessor(IQueueClient queueClient)
        {
            var clientFactory = new Mock<IQueueClientFactory>();
            clientFactory.Setup(m => m.Create())
                .Returns(queueClient);

            var registry = new Mock<IQueueMessageMapperRegistry>();
            registry.Setup(m => m.GetMapper(It.IsAny<Type>()))
                .Returns(typeof(TestRequestMapper));

            var factory = new Mock<IQueueMessageMapperFactory>();
            factory.Setup(m => m.Create(It.IsAny<Type>()))
                .Returns(new TestRequestMapper());

            return new QueueProcessor(registry.Object, factory.Object, clientFactory.Object);
        }

        [Fact]
        public async Task EnqueueAsync_NullRequest_ThrowsException()
        {
            var client = new InMemoryQueueClient();
            var processor = BuildQueueProcessor(client);

            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await processor.EnqueueAsync(null));
        }

        [Fact]
        public async Task EnqueueAsync_Request_QueuesRequest()
        {
            var client = new InMemoryQueueClient();
            var processor = BuildQueueProcessor(client);

            await processor.EnqueueAsync(new TestRequest());

            Assert.Single(client.Queue);
        }
    }
}
