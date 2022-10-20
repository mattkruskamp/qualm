using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qualm.Queuing
{
    public class QueueMessageMapperRegistry : IQueueMessageMapperRegistry
    {
        readonly IServiceCollection _services;
        readonly Dictionary<Type, Type> _types;

        public QueueMessageMapperRegistry(
            IServiceCollection services)
        {
            _services = services;
            _types = new Dictionary<Type, Type>();
        }

        public virtual Type? GetMapper(Type requestType) =>
            _types.ContainsKey(requestType) ? _types[requestType] : null;

        public virtual void RegisterMapper(Type requestType, Type mapperType)
        {
            if (requestType == null)
                throw new InvalidOperationException("reqeustType cannot be null");
            if (mapperType == null)
                throw new InvalidOperationException("mapperType cannot be null");

            if (!typeof(IRequest).IsAssignableFrom(requestType))
                throw new InvalidOperationException(
                    $"{requestType} must be a IRequest to be registered");

            if (!typeof(IQueueMessageMapper).IsAssignableFrom(mapperType))
                throw new InvalidOperationException(
                    $"{mapperType} must be a IQueueMessageMapper to be registered");

            _types.Add(requestType, mapperType);
            _services.Add(new ServiceDescriptor(mapperType, mapperType, ServiceLifetime.Transient));
        }

        public virtual void RegisterMappers(params Assembly[] assemblies)
        {
            var subscribers =
                from t in assemblies.SelectMany(a => a.ExportedTypes)
                let ti = t.GetTypeInfo()
                where ti.IsClass && !ti.IsAbstract && !ti.IsInterface
                from i in t.GetTypeInfo().ImplementedInterfaces
                where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueueMessageMapper<>)
                select new
                {
                    QueryType = i.GenericTypeArguments.ElementAt(0),
                    RequestType = t
                };

            foreach (var subscriber in subscribers)
            {
                RegisterMapper(subscriber.QueryType, subscriber.RequestType);
            }
        }
    }
}
