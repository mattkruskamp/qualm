﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace Qualm.Commands.DependencyInjection
{
    public class ServiceCollectionCommandHandlerRegistry : CommandHandlerRegistry
    {
        private readonly IServiceCollection _services;
        private readonly ServiceLifetime _lifetime;

        public ServiceCollectionCommandHandlerRegistry(
            IServiceCollection services,
            ServiceLifetime lifetime)
        {
            _services = services;
            _lifetime = lifetime;
        }

        public override void RegisterHandler(
            Type commandType, Type commandHandlerType)
        {
            base.RegisterHandler(commandType, commandHandlerType);

            _services.Add(new ServiceDescriptor(commandHandlerType,
                commandHandlerType, _lifetime));
        }
    }
}
