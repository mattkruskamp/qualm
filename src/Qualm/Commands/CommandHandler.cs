using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Commands
{
    public abstract class CommandHandler<TCommand>
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract Task ExecuteAsync(TCommand command,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
