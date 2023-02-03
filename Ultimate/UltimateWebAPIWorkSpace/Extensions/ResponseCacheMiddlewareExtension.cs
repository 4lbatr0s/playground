using Microsoft.AspNetCore.ResponseCaching;

namespace UltimateWebAPIWorkSpace.Extensions;

public static class ResponseCacheMiddlewareExtension
{
    public static void ConfigureResponseCacheMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
            if (responseCachingFeature != null)
            {
                context.Response.Headers.Add("X-Response-Cache", "HIT");
            }
            else
            {
                context.Response.Headers.Add("X-Response-Cache", "MISS");
                await next();
            }
        });
    }
}