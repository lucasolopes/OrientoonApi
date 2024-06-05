using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OrientoonApi.Infrastructure.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JsonSerializationException jsonException)
            {
                context.Result = new BadRequestObjectResult(new { error = jsonException.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
