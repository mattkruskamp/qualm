using Microsoft.Extensions.DependencyInjection;

namespace Qualm.Queries.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IQueryHandlerRegistry AddQueries(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var registry = new ServiceCollectionQueryHandlerRegistry(services, lifetime);

            services.AddSingleton<IQueryHandlerRegistry>(m => registry);
            services.AddScoped<IQueryHandlerFactory, ServiceProviderQueryHandlerFactory>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();

            return registry;
        }
    }
}
