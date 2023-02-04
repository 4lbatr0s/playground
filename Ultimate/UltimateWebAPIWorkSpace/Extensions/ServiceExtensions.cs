using System.Text;
using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using LoggingService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

    public static void ConfigureEmployeeLinks(this IServiceCollection services) =>
        services.AddScoped<IEmployeeLinks, EmployeeLinks>();


    //INFO: For adding Formatters (Content Negotiation), add it to Controllers in Program.cs
    public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
    builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

    //INFO: FOR HATEOAS: this will create custom media types!
    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config =>
        {
            var systemTextJsonOutputFormatter = config.OutputFormatters
            .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

            if (systemTextJsonOutputFormatter != null)
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

    //INFO: For API Versioning.
    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true; //TIP: Adds the API version to the Response header
            opt.AssumeDefaultVersionWhenUnspecified = true; //TIP: Specifies the default version if client doesnt send one
            opt.DefaultApiVersion = new ApiVersion(1, 0); //TIP: sets the default version count.
        });
    }


    //INFO: For Response Caching: CACHE STORE, without this, cache-control header wont be enough for caching
    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

    //INFO: Marvin.Cache.Headers lib configurations
    public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
    {
        services.AddHttpCacheHeaders(
            (expirationOpt) =>
            {
                expirationOpt.MaxAge = 65;
                expirationOpt.CacheLocation = CacheLocation.Private; //TIP: if we do it private, it wont cache it!
            },
            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            }
        );
    }

    //INFO: To implement throttling
    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {   //TIP: first install the AspNetCoreRateLimit package!
        //rules
        var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 30,
                    Period = "5m"
                }
            };
        services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules = rateLimitRules; });
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        /*
        We create a rate limit rules first, for now just one, stating that three
        requests are allowed in a five-minute period for any endpoint in our API.
        Then, we configure IpRateLimitOptions to add the created rule. Finally, we
        have to register rate limit stores, configuration, and processing strategy
        as a singleton. They serve the purpose of storing rate limit counters and
        policies as well as adding configuration.
        */
    }


    //INFO: To configure ASPNET CORE IDENTITY
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.Password.RequiredLength = 10;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();
    }

    //INFO: To configure JWT
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        // var secretKey = Environment.GetEnvironmentVariable("SECRET");
        var secretKey = jwtSettings["secretKey"];
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            /*
            INFO:
            The "issuer" is the entity that generates and signs the JWT,
            while the "audience" is the intended recipient of the JWT. 
            The recipient can then use the information in the JWT to perform some actions.
            The audience and issuer are included in the claims of the JWT and are used to validate the authenticity
            and purpose of the token.
            */
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                /*
                INFO:
                Additionally, we are providing values for the issuer, the audience, and the
                secret key that the server uses to generate the signature for JWT.
                */
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                ClockSkew = TimeSpan.FromSeconds(5),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }

}