﻿using System;

namespace Qualm.Commands.DependencyInjection
{
    public class ServiceProviderCommandHandlerFactory
        : ICommandHandlerFactory
    {
        readonly IServiceProvider _provider;

        public ServiceProviderCommandHandlerFactory(
            IServiceProvider provider)
        {
            _provider = provider;
        }

        public ICommandHandler Create(Type handlerType)
        {
            if (handlerType == null)
                throw new InvalidOperationException("handlerType cannot be null");

            if (!typeof(ICommandHandler).IsAssignableFrom(handlerType))
                throw new InvalidOperationException(
                    $"{handlerType.Name} must be an ICommandHandler");

            var service = _provider.GetService(handlerType);
            return (service as ICommandHandler)!;
        }
    }
}
