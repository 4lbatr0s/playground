namespace UltimateWebAPIWorkSpace.Extensions;
using Contracts;
using LoggingService;
/**
 * INFO:We will use this class to introduce our services to Program.cs file.
 * Should be static.
 */
public static class ServiceExtensions
{
    //INFO: An Extension method takes this as the first parameter!
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

    public static void ConfigureLoggerService(this IServiceCollection services)=>
        services.AddSingleton<ILoggerManager, LoggerManager>();  
}