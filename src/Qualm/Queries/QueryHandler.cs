using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queries
{
    public abstract class QueryHandler<TQuery, TResult>
        : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public abstract Task<TResult> ExecuteAsync(TQuery query,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
