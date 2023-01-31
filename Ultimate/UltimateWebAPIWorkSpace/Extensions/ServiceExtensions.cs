using Contracts;
using LoggingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using UltimateWebAPIWorkSpace.Formatters.Csv;
using UltimateWebAPIWorkSpace.Utilities;
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
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                    );
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
    services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
    services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
    services.AddScoped<IServiceManager, ServiceManager>();
    

    //INFO: To make RepositoryContext run in Runtime instead of Design time:
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
    services.AddDbContext<RepositoryContext>(opts =>
        opts.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"))
    );

    public static void ConfigureEmployeeLinks(this IServiceCollection services)=>
        services.AddScoped<IEmployeeLinks, EmployeeLinks>();


    //INFO: For adding Formatters (Content Negotiation), add it to Controllers in Program.cs
    public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
    builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

    //INFO: FOR HATEOAS: this will create custom media types!
    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config => {
            var systemTextJsonOutputFormatter = config.OutputFormatters
            .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

            if(systemTextJsonOutputFormatter != null)
            {
                systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.ultimate.hateoas+json");
                systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.ultimate.apiroot+json");
            }
            var xmlOutputFormatter = config.OutputFormatters
            .OfType<XmlDataContractSerializerOutputFormatter>()?
            .FirstOrDefault();

            if (xmlOutputFormatter != null)
            {
                xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.ultimate.hateoas+xml");
                xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.ultimate.apiroot+xml");
            }
        });
    }
}