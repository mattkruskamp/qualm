using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queuing
{
    public interface IQueueClient
    {
        Task SendAsync(QueueMessage message,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
