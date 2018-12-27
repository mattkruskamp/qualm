using Qualm.Queuing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Tests.Queuing
{
    public class InMemoryQueueClient : IQueueClient
    {
        private readonly Queue<QueueMessage> _queue;

        public InMemoryQueueClient()
        {
            _queue = new Queue<QueueMessage>();
        }

        public async Task SendAsync(QueueMessage message, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => _queue.Enqueue(message));
        }

        public Queue<QueueMessage> Queue
        {
            get { return _queue; }
        }
    }
}
