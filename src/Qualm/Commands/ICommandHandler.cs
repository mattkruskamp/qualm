using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Commands
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command, 
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
