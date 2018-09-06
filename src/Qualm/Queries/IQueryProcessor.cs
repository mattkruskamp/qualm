using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
