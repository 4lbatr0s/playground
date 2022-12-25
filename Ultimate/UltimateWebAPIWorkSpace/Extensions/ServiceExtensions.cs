using System;
using Contracts;
using LoggingService;
using Repository;
using Service;
using Service.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

/**
 * INFO:We will use this class to introduce our services to Program.cs file.
 * Should be static.
 */
namespace UltimateWebAPIWorkSpace.Extensions;

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

    public static void ConfigureRepositoryManager(this IServiceCollection services)=>
    services.AddScoped<IRepositoryManager, RepositoryManage>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
    services.AddScoped<IServiceManager, ServiceManager>();

    //INFO: To make RepositoryContext run in Runtime instead of Design time:
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)=>
    services.AddDbContext<RepositoryContext>(opts =>
        opts.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"))
    );
}