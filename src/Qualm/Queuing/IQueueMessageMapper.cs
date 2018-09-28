using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Queuing
{
    public interface IQueueMessageMapper
    {
        IQueueMessage ToMessage(IRequest request);

        IRequest ToReqeust(IQueueMessage message);
    }
}
