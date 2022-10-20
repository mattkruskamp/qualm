using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        readonly IQueryHandlerRegistry _registry;
        readonly IQueryHandlerFactory _factory;

        public QueryProcessor(
            IQueryHandlerRegistry registry,
            IQueryHandlerFactory factory)
        {
            _registry = registry;
            _factory = factory;
        }

        public async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (query == null)
                throw new InvalidOperationException($"Query cannot be null");

            var handler = GetHandler(query);

            var function = new Func<IQuery<TResult>, CancellationToken, Task<TResult>>(
                (r, ct) => ((dynamic)handler).ExecuteAsync((dynamic)r, ct));

            return await function.Invoke(query, cancellationToken);
        }

        private IQueryHandler GetHandler<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            var handlerType = _registry.GetHandler(queryType)!;
            var handler = _factory.Create(handlerType);

            return handler;
        }
    }
}
