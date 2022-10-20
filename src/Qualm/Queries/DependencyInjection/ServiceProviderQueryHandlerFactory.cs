using System;

namespace Qualm.Queries.DependencyInjection
{
    public class ServiceProviderQueryHandlerFactory
        : IQueryHandlerFactory
    {
        readonly IServiceProvider _provider;

        public ServiceProviderQueryHandlerFactory(
            IServiceProvider provider)
        {
            _provider = provider;
        }

        public IQueryHandler Create(Type handlerType)
        {
            if (handlerType == null)
                throw new InvalidOperationException("handlerType cannot be null");

            if (!typeof(IQueryHandler).IsAssignableFrom(handlerType))
                throw new InvalidOperationException(
                    $"{handlerType.Name} must be an IQueryHandler");

            var service = _provider.GetService(handlerType);
            return (service as IQueryHandler)!;
        }
    }
}
