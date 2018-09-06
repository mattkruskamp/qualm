using System;
using System.Reflection;

namespace Qualm.Commands
{
    public interface ICommandHandlerRegistry
    {
        Type GetHandler(Type commandType);

        void RegisterHandler(Type commandType, Type commandHandlerType);

        void RegisterHandlers(params Assembly[] assemblies);
    }
}
