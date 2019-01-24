using Qualm.AspNetCore.Swagger.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qualm.AspNetCore.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (context.SystemType.IsEnum(out var enumName))
                model.Extensions.Add("x-ms-enum", new
                {
                    name = enumName ?? context.SystemType.Name,
                    modelAsString = false
                });
        }
    }
}

