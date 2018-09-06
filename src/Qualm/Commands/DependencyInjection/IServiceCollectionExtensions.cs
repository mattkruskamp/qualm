using Microsoft.Extensions.DependencyInjection;

namespace Qualm.Commands.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static ICommandHandlerRegistry AddCommands(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var registry = new ServiceCollectionHandlerRegistry(services, lifetime);

            services.AddSingleton<ICommandHandlerRegistry>(m => registry);
            services.AddScoped<ICommandHandlerFactory, ServiceProviderHandlerFactory>();
            services.AddScoped<ICommandProcessor, CommandProcessor>();

            return registry;
        }
    }
}
