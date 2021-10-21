using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Qualm.AspNetCore.Swagger.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qualm.AspNetCore.Swagger
{
    public class EnumParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;

            if (type.IsEnum(out var enumName))
                parameter.Extensions.Add("x-ms-enum", new OpenApiObject()
                {
                    { "name", new OpenApiString(enumName ?? type.Name) },
                    { "modelAsString", new OpenApiBoolean(false) }
                });
        }
    }
}
