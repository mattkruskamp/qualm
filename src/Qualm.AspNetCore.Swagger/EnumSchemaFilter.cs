using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Qualm.AspNetCore.Swagger.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qualm.AspNetCore.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum(out var enumName))
                schema.Extensions.Add("x-ms-enum", new OpenApiObject()
                {
                    { "name", new OpenApiString(enumName ?? context.Type.Name) },
                    { "modelAsString", new OpenApiBoolean(false) }
                });
        }
    }
}
