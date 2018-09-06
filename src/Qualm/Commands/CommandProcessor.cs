using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandHandlerRegistry _registry;
        private readonly ICommandHandlerFactory _factory;

        public CommandProcessor(
            ICommandHandlerRegistry registry,
            ICommandHandlerFactory factory)
        {
            _registry = registry;
            _factory = factory;
        }

        public async Task ExecuteAsync(ICommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (command == null)
                throw new InvalidOperationException($"Command cannot be null");

            var handler = GetHandler(command);

            var function = new Func<ICommand, CancellationToken, Task>(
                (r, ct) => ((dynamic)handler).ExecuteAsync((dynamic)r, ct));

            await function.Invoke(command, cancellationToken);
        }

        private ICommandHandler GetHandler(ICommand command)
        {
            var type = command.GetType();
            var handlerType = _registry.GetHandler(type);
            var handler = _factory.Create(handlerType);

            return handler;
        }
    }
}
