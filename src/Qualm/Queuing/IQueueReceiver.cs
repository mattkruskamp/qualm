using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Queuing
{
    public interface IQueueReceiver
    {
        QueueMessage[] Receive();
    }
}
