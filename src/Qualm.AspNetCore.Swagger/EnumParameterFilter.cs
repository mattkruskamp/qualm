using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Qualm.AspNetCore.Swagger
{
    public class EnumParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;

            if (type.IsEnum)
            {
                Enum.GetNames(type)
                    .ToList()
                    .ForEach(n => parameter.Extensions.Add("x-ms-enum", new OpenApiString(n)));
            }
        }
    }
}
