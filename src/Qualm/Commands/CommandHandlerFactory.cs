using System;

namespace Qualm.Commands
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public CommandHandlerFactory()
        {
            
        }

        public virtual ICommandHandler Create(Type handlerType)
        {
            if (handlerType == null)
                throw new InvalidOperationException("handlerType cannot be null");

            if (!typeof(ICommandHandler).IsAssignableFrom(handlerType))
                throw new InvalidOperationException($"{handlerType.Name} must be an ICommandHandler");

            ICommandHandler instance = (ICommandHandler)Activator.CreateInstance(handlerType);

            return instance;
        }
    }
}
