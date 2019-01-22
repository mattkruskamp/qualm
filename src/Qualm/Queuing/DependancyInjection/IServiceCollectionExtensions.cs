using Microsoft.Extensions.DependencyInjection;

namespace Qualm.Queuing.DependancyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IQueueMessageMapperRegistry AddQueueing(this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var registry = new QueueMessageMapperRegistry(services);

            services.AddSingleton<IQueueMessageMapperRegistry>(m => registry);
            services.AddTransient<IQueueMessageMapperFactory, QueueMessageMapperFactory>();
            services.Add(new ServiceDescriptor(typeof(IQueueProcessor), typeof(QueueProcessor), lifetime));

            return registry;
        }
    }
}
