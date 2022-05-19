using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            //the more you create a better log name, the more you make your logs effective.
            var watch = Stopwatch.StartNew();
            try //we create try catch blocks here to get rid of try catch blocks in the controllers.
            {
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);
                await _next.Invoke(context);// call the next middleware.
                watch.Stop(); //stop watch
                //create response log.
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.ElapsedMilliseconds;
                _loggerService.Write(message);
            }
            catch (System.Exception ex)
            {
                //if there is an exception, then throw it.
                watch.Stop();
                await HandleException(context, ex, watch);
            }
             
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//if there is a problem, return internal error(500)

            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message: " + ex.Message + " in " + watch.ElapsedMilliseconds + " ms";  
            _loggerService.Write(message);
            
            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None); //return exception as a json for ui
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}