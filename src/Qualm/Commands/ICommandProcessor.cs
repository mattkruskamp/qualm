using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Commands
{
    public interface ICommandProcessor
    {
        Task ExecuteAsync(IRequest command,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
