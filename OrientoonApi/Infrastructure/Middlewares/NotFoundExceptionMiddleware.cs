using Newtonsoft.Json;
using OrientoonApi.Infrastructure.Exceptions;
using System.Net;

namespace OrientoonApi.Infrastructure.Middlewares
{
    public class NotFoundExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
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
