using Qualm.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Tests.Queries
{
    public class TestQueryHandler : QueryHandler<TestQuery, string>
    {
        public override async Task<string> ExecuteAsync(TestQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => "Hello World");
        }
    }
}
