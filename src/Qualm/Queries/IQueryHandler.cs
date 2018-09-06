using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queries
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler
        where TQuery : IQuery<TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
