using Microsoft.Extensions.DependencyInjection;
using Qualm.Queuing;

namespace Qualm.Rmq.DependancyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRmqQueueing(
            this IServiceCollection services,
            RmqConnectionDetails options)
        {
            services.AddSingleton(options);
            services.AddSingleton<RmqConnectionFactory>();
            services.AddSingleton<RmqChannelFactory>();
            services.AddSingleton<RmqDispatcher>();

            services.AddScoped<RmqQueueClient>();
            services.AddScoped<IQueueClientFactory, RmqQueueClientFactory>();

            return services;
        }
    }
}
