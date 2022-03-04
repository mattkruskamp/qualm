using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qualm.AspNetCore.Swagger
{
    /// <summary>
    /// The workaround for openapi3/autorest query parameters serialization
    /// See more: https://github.com/Azure/autorest/issues/3373, https://swagger.io/specification/
    /// </summary>
    public class ArrayInQueryParametersFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.In.HasValue && parameter.In.Value == ParameterLocation.Query)
            {
                if (parameter.Schema?.Type == "array")
                {

                    parameter.Style = ParameterStyle.Form;
                    parameter.Explode = true;
                    parameter.Extensions.Add("explode", new ExplodeExtension());
                }
            }
        }
    }
}