using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrientoonApi.Utils
{
    public class SwaggerDateFormatSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Type == "string" && (context.Type == typeof(DateTime) || context.Type == typeof(DateTime?)))
            {
                schema.Example = new OpenApiString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
        }
    }
}
