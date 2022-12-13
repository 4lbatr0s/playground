var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) => //TIP: app.Use is a Func delegate that accepts a HTTPContext and a Func delegate as a parameter. Its return type is Task!
{
    Console.WriteLine($"Logic before executing the next delegate in the Use method");
    await next.Invoke(); //next paraemeter
    Console.WriteLine($"Logic after executing the next delegate in the Use method");
});

//INFO: app.Map helps us to create a pipeline only special to a pathMath!
app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logic in the Use method before the next delegate");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the Use method after the next delegate");
    });
    builder.Run(async context =>
    {
        Console.WriteLine($"Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the map branch.");
    });
});

//INFO:Lets create our own Middleware
app.Run(async context => //INFO: Run delegate accepts a  RequestDelegate parameter, RequestDelegate parameter accepts a HTTPContext parameter!
{
    await context.Response.WriteAsync("Hello from the middleware component"); //INFO: So we are using that CONTEXT parameter to modify requests and responses in the middleware.
});

app.MapControllers();

app.Run();
