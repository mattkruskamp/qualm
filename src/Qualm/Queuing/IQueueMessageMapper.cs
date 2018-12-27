using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Queuing
{
    public interface IQueueMessageMapper { }

    /// <summary>
    /// Converts a request to a queue message.
    /// </summary>
    public interface IQueueMessageMapper<TRequest> : IQueueMessageMapper
        where TRequest : class, IRequest
    {
        QueueMessage ToMessage(TRequest request);

        TRequest ToRequest(QueueMessage message);
    }
}
