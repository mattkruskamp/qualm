using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Queries
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        public QueryHandlerFactory()
        {
        }

        public IQueryHandler Create(Type handlerType)
        {
            if (handlerType == null)
                throw new InvalidOperationException("handlerType cannot be null");

            if (!typeof(IQueryHandler).IsAssignableFrom(handlerType))
                throw new InvalidOperationException($"{handlerType.Name} must be an ICommandHandler");

            IQueryHandler instance = (IQueryHandler)Activator.CreateInstance(handlerType);

            return instance;
        }
    }
}
