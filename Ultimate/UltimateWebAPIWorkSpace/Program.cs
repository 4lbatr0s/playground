using Microsoft.AspNetCore.HttpOverrides;
using UltimateWebAPIWorkSpace.Extensions;
using NLog;
/**
 * INFO:builder helps us to add Configurations, Services, Loggin Configurations, IHostBuilder and IWebHostBuilder 
 */
var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));//INFO: Get the logging configs.

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build(); //INFO:Equivalent of the Configure method in NET5. It's literally our web application instance.


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();//TIP:Mandatory, use it.
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();//TIP:adds the Strict Transport Security Header!
}

app.UseHttpsRedirection();//TIP: Redirects from HTTP to HTTPS.
app.UseStaticFiles();//TIP:Enables using static files for the request, if we dont set a path for the files it will use wwwroot.
app.UseForwardedHeaders(new ForwardedHeadersOptions()//TIP:Will forward proxy headers to the current request, it will help us during deployment!
{
    ForwardedHeaders = ForwardedHeaders.All 
});
app.UseCors("CorsPolicy");//TIP:Mandatory, use it.
app.UseAuthorization();

//INFO: If we want to add custom middlewares they should place between authorizaton and map controllers.

app.MapControllers();//TIP: Gets endpoints from Controller actions and pass them to IEndpointRouteBuilder.  

app.Run();
