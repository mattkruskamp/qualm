using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Qualm.Queries
{
    public class QueryHandlerRegistry : IQueryHandlerRegistry
    {
        readonly Dictionary<Type, Type> _types;

        public QueryHandlerRegistry() =>
            _types = new Dictionary<Type, Type>();

        public virtual Type? GetHandler(Type queryType) =>
            _types!.ContainsKey(queryType) ? _types[queryType] : null;

        public virtual void RegisterHandler(Type queryType, Type queryHandlerType)
        {
            if (queryType == null)
                throw new InvalidOperationException("queryType cannot be null");
            if (queryHandlerType == null)
                throw new InvalidOperationException("queryHandlerType cannot be null");

            if (!typeof(IQuery).IsAssignableFrom(queryType))
                throw new InvalidOperationException(
                    $"{queryType.Name} must be an IQuery to be registered");
            if (!typeof(IQueryHandler).IsAssignableFrom(queryHandlerType))
                throw new InvalidOperationException(
                    $"{queryHandlerType.Name} must be an IQueryHandler to be registered");

            _types.Add(queryType, queryHandlerType);
        }

        public void RegisterHandlers(params Assembly[] assemblies)
        {
            var subscribers =
                from t in assemblies.SelectMany(a => a.ExportedTypes)
                let ti = t.GetTypeInfo()
                where ti.IsClass && !ti.IsAbstract && !ti.IsInterface
                from i in t.GetTypeInfo().ImplementedInterfaces
                where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)
                select new
                {
                    QueryType = i.GenericTypeArguments.ElementAt(0),
                    ResultType = i.GenericTypeArguments.ElementAt(1),
                    HandlerType = t
                };

            foreach (var subscriber in subscribers)
            {
                RegisterHandler(subscriber.QueryType, subscriber.HandlerType);
            }
        }
    }
}
