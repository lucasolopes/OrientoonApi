using Newtonsoft.Json;
using OrientoonApi.Infrastructure.Exceptions;
using System.Net;

namespace OrientoonApi.Infrastructure.Middlewares
{
    public class ConflictMiddleware
    {
        private readonly RequestDelegate _next;
        public ConflictMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ConflictException ex)
            {
                await HandleConflictExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleConflictExceptionAsync(HttpContext context, ConflictException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var result = JsonConvert.SerializeObject(new
            {
                message = exception.Message,
                statusCode = context.Response.StatusCode
            });

            return context.Response.WriteAsync(result);
        }
    }
}
