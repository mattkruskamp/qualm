using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qualm.AspNetCore.Swagger
{
    public class AllOfDocumentFilter<T> : IDocumentFilter
    {
        private readonly Type _type;

        public AllOfDocumentFilter()
        {
            _type = typeof(T);
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            RegisterType(context.SchemaRegistry, _type);
        }

        private void RegisterType(ISchemaRegistry schemaRegistry, Type t)
        {
            var baseType = t.BaseType;

            if (baseType == null)
                return;

            var assemblies = new List<Assembly>()
            {
                t.Assembly
            };

            // don't include base type assembly
            // if its the same as the child type
            // or if base type is object
            if (baseType.Assembly != t.Assembly &&
                baseType != typeof(object))
            {
                assemblies.Add(baseType.Assembly);
            }

            List<Type> derivedTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly
                    .GetTypes()
                    .Where(p => p.BaseType == baseType);

                derivedTypes.AddRange(types);
            }

            foreach (var dt in derivedTypes)
                schemaRegistry.GetOrRegister(dt);

            RegisterType(schemaRegistry, baseType);
        }
    }
}
