using System;

namespace Qualm.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler Create(Type handlerType);
    }
}
