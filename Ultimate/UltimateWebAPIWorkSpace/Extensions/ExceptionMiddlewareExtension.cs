using System.Diagnostics.CodeAnalysis;
using System.Net;
using Contracts;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Models.ErrorModel;

namespace UltimateWebAPIWorkSpace.Extensions;

//INFO: How to create a global exception middleware!
public static class ExceptionMiddlewareExtension
{
    //INFO:UseExceptionHandler is a built in ExceptionHandler 
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler((appError) =>//INFO: Another exception method.
        {
            //TIP: Run is used with Middlewares, that means here we've created a Middleware.
            //INFO: Run is used on the terminal middlewares,        
            appError.Run(async context => //TIP:appError is an action of IApplicationBuilder type.
            {
                // context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    //INFO: For collection, if we send bad request, return bad request not 500 internal server error.
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        //TIP: For Exceptions that are inherited from NotFoundException, throw 404
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    await context.Response.WriteAsync(new ErrorDetail()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message //TIP: return exception message specific to the problem.
                    }.ToString());

                }
            });
        });
    }
}
