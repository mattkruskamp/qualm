using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queuing
{
    public interface IQueueProcessor
    {
        Task EnqueueAsync(IRequest request,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
