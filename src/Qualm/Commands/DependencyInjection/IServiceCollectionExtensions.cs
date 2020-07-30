using Microsoft.Extensions.DependencyInjection;

namespace Qualm.Commands.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static ICommandHandlerRegistry AddCommands(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var registry = new ServiceCollectionCommandHandlerRegistry(services, lifetime);

            services.AddSingleton<ICommandHandlerRegistry>(m => registry);
            services.AddScoped<ICommandHandlerFactory, ServiceProviderCommandHandlerFactory>();
            services.AddScoped<ICommandProcessor, CommandProcessor>();

            return registry;
        }

        public static IServiceCollection AddRmq(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            
            return services;            
        }
    }
}
