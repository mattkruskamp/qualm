using System;

namespace Qualm.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler Create(Type handlerType);
    }
}
