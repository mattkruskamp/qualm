using System;

namespace Qualm.Queuing
{
    public interface IQueueMessageMapperFactory
    {
        IQueueMessageMapper Create(Type messageMapperType);
    }
}
