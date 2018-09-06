using Qualm.Queries;

namespace Qualm.Tests.Queries
{
    public class TestQuery : IQuery<string>
    {
        public string Parameter { get; set; }
    }
}
