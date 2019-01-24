using Qualm.AspNetCore.Swagger.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Qualm.AspNetCore.Swagger
{
    public class EnumParameterFilter : IParameterFilter
    {
        public void Apply(IParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;

            if (type.IsEnum(out var enumName))
                parameter.Extensions.Add("x-ms-enum", new { name = enumName ?? type.Name, modelAsString = false });
        }
    }
}
