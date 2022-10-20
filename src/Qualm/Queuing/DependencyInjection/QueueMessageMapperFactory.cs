using System;

namespace Qualm.Queuing
{
    public class QueueMessageMapperFactory : IQueueMessageMapperFactory
    {
        readonly IServiceProvider _provider;

        public QueueMessageMapperFactory(
            IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public IQueueMessageMapper Create(Type mapperType)
        {
            if (mapperType == null)
                throw new InvalidOperationException("handlerType cannot be null");

            if (!typeof(IQueueMessageMapper).IsAssignableFrom(mapperType))
                throw new InvalidOperationException(
                    $"{mapperType.Name} must be an IQueueMessageMapper");

            var service = _provider.GetService(mapperType);
            return (service as IQueueMessageMapper)!;
        }
    }
}
