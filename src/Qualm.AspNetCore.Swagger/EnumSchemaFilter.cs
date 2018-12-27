using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qualm.AspNetCore.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (context.SystemType.IsEnum)
                model.Extensions.Add("x-ms-enum", new
                {
                    name = context.SystemType.Name,
                    modelAsString = false
                });
        }
    }
}
