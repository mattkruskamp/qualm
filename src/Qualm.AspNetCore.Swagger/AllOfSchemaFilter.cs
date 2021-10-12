using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qualm.AspNetCore.Swagger
{
    /// <summary>
    /// Custom schema filter that includes types in an inheritance chain from
    /// external assemblies.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AllOfSchemaFilter<T> : ISchemaFilter
    {
        private readonly Type _type;
        private readonly Lazy<IDictionary<Type, Type>> _types;

        public AllOfSchemaFilter()
        {
            _type = typeof(T);
            _types = new Lazy<IDictionary<Type, Type>>(MapTypes);
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!_types.Value.ContainsKey(context.Type))
                return;

            var clonedSchema = new OpenApiSchema
            {
                Properties = schema.Properties,
                Required = schema.Required,
                Type = schema.Type
            };

            var baseType = _types.Value[context.Type];

            var parentSchema = new OpenApiSchema
            {
                Reference = new OpenApiReference() { ExternalResource = $"#/definitions/{baseType.Name}" }
            };

            schema.AllOf = new List<OpenApiSchema>
            {
                parentSchema,
                clonedSchema
            };

            // gets base class property names
            // and converts them to camel case so
            // they match swagger generated schema
            // properties
            var basePropertyNames = baseType
                .GetProperties()
                .Select(p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1))
                .ToList();

            // since the allOf definition added above assumes base
            // class properties we need to remove them from the sub
            // class
            foreach (var n in basePropertyNames)
            {
                schema.Properties.Remove(n);
            }
        }

        private IDictionary<Type, Type> MapTypes()
        {
            var typeStore = new Dictionary<Type, Type>();

            MapType(typeStore, _type);

            return typeStore;
        }

        private void MapType(
            IDictionary<Type, Type> typeStore,
            Type typ)
        {
            var assemblies = new Assembly[]
            {
                typ.Assembly, typ.BaseType.Assembly
            }.Distinct();

            // stop at object, object inheritance is implied
            if (typ.BaseType == typeof(object) || typeStore.ContainsKey(typ))
                return;

            var baseType = typ.BaseType;

            List<Type> derivedTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly
                    .GetTypes()
                    .Where(t => t.BaseType == baseType);

                derivedTypes.AddRange(types);
            }

            foreach (var t in derivedTypes)
            {
                typeStore.Add(t, t.BaseType);
            }

            MapType(typeStore, typ.BaseType);
        }
    }
}
