using Microsoft.Extensions.DependencyInjection;
using Qualm.Queries;
using Qualm.Queries.DependencyInjection;
using Xunit;

namespace Qualm.Tests.Queries.DependencyInjection
{
    public class IServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddQueries_NoParams_RegistersDependencies()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddQueries();
            var provider = services.BuildServiceProvider();
            var registry = provider.GetService<IQueryHandlerRegistry>();
            var factory = provider.GetService<IQueryHandlerFactory>();
            var processor = provider.GetService<IQueryProcessor>();
        }
    }
}
