using System;
using System.Reflection;

namespace Qualm.Queries
{
    public interface IQueryHandlerRegistry
    {
        Type GetHandler(Type queryType);

        void RegisterHandler(Type queryType, Type queryHandlerType);

        void RegisterHandlers(params Assembly[] assemblies);
    }
}
