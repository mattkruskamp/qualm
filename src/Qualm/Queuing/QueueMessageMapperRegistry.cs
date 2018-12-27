using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Queuing
{
    public class QueueMessageMapperRegistry
        : IQueueMessageMapperRegistry
    {
        private readonly Dictionary<Type, Type> _types;

        public QueueMessageMapperRegistry()
        {
            _types = new Dictionary<Type, Type>();
        }

        public Type GetMapper(Type requestType)
        {
            return _types.ContainsKey(requestType) ? _types[requestType] : null;
        }

        public void RegisterMapper(Type requestType, Type messageMapperType)
        {
            if (requestType == null)
                throw new InvalidOperationException($"requestType cannot be null");
            if (messageMapperType == null)
                throw new InvalidOperationException($"requestMapperType cannot be null");

            if (!typeof(IRequest).IsAssignableFrom(requestType))
                throw new InvalidOperationException(
                    $"{requestType.Name} must be an IRequest to be registered");

            if (!typeof(QueueMessage).IsAssignableFrom(messageMapperType))
                throw new InvalidOperationException(
                    $"{messageMapperType.Name} must be an IQueueMessageMapper to be registered");
        }
    }
}
