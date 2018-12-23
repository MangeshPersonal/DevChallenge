using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TODO.MODELS.ResponseModel;
namespace TODO.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleWare(RequestDelegate next,ILogger ilogger)
        {
            _next = next;
            _logger = ilogger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Procssing Request", null);
                await HandleError(httpContext, ex);
            }

        }

        public static async Task HandleError(HttpContext context, Exception exception)
        {
            var jsonresult = new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.InternalServerError, result:
                     "Some Exception Occured" + exception.Message, errorMessage: exception.Message);

            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(jsonresult));

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleWare>();
        }
    }
}
