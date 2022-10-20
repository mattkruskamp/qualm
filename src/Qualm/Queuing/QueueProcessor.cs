using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queuing
{
    public class QueueProcessor : IQueueProcessor
    {
        readonly IQueueClientFactory _clientFactory;
        readonly IQueueMessageMapperRegistry _mapperRegistry;
        readonly IQueueMessageMapperFactory _mapperFactory;

        public QueueProcessor(
            IQueueMessageMapperRegistry mapperRegistry,
            IQueueMessageMapperFactory mapperFactory,
            IQueueClientFactory clientFactory)
        {
            _mapperRegistry = mapperRegistry;
            _mapperFactory = mapperFactory;
            _clientFactory = clientFactory;
        }

        public async Task EnqueueAsync(IRequest request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null)
                throw new InvalidOperationException($"Request cannot be null");

            var mapperType = _mapperRegistry.GetMapper(request.GetType())!;
            var mapper = _mapperFactory.Create(mapperType);

            var function = new Func<IRequest, QueueMessage>(
                (t) => ((dynamic)mapper).ToMessage((dynamic)request));

            var message = function.Invoke(request);

            var client = _clientFactory.Create();
            await client.SendAsync(message, cancellationToken);
        }
    }
}
