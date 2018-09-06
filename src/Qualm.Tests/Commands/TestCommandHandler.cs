using Qualm.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Tests.Commands
{
    public class TestCommandHandler : CommandHandler<TestCommand>
    {
        public override async Task ExecuteAsync(TestCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => command.CommandNumber = 20);
        }
    }
}
