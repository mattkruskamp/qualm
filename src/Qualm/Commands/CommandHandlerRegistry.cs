using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qualm.Commands
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        readonly Dictionary<Type, Type> _types;

        public CommandHandlerRegistry() =>
            _types = new Dictionary<Type, Type>();

        public virtual Type? GetHandler(Type commandType) => 
            _types!.ContainsKey(commandType) ? _types[commandType] : null;

        public virtual void RegisterHandler(Type commandType, Type commandHandlerType)
        {
            if (commandType == null)
                throw new InvalidOperationException("commandType cannot be null");
            if (commandHandlerType == null)
                throw new InvalidOperationException("commandHandlerType cannot be null");

            if (!typeof(ICommand).IsAssignableFrom(commandType))
                throw new InvalidOperationException(
                    $"{commandType.Name} must be an ICommand to be registered");

            if (!typeof(ICommandHandler).IsAssignableFrom(commandHandlerType))
                throw new InvalidOperationException(
                    $"{commandHandlerType.Name} must be an ICommandHandler to be registered");

            _types.Add(commandType, commandHandlerType);
        }

        public virtual void RegisterHandlers(params Assembly[] assemblies)
        {
            var subscribers =
                from t in assemblies.SelectMany(a => a.ExportedTypes)
                let ti = t.GetTypeInfo()
                where ti.IsClass && !ti.IsAbstract && !ti.IsInterface
                from i in t.GetTypeInfo().ImplementedInterfaces
                where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                select new
                {
                    QueryType = i.GenericTypeArguments.ElementAt(0),
                    HandlerType = t
                };

            foreach (var subscriber in subscribers)
            {
                RegisterHandler(subscriber.QueryType, subscriber.HandlerType);
            }
        }
    }
}
