using MiddlewarePractices.Middlewares;

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


//app.Use function
// app.Use(async (context, next) => {
//     System.Console.WriteLine("Middleware 1 basladi");
//     await next.Invoke(); 
//     System.Console.WriteLine("Middleware 1 sonlandi");
// }); 
// app.Use(async (context, next) => {
//     System.Console.WriteLine("Middleware 2 basladi");
//     await next.Invoke(); 
//     System.Console.WriteLine("Middleware 2 sonlandi");
// }); 
// app.Use(async (context, next) => {
//     System.Console.WriteLine("Middleware 3 basladi");
//     await next.Invoke(); 
//     System.Console.WriteLine("Middleware 3 sonlandi");
// }); 

//**************************************
//custom middleware
app.UseHello();

//**************************************
//app.Map Function.
app.Use(async (context, next) =>
{
    System.Console.WriteLine("Use middleware triggered!");
    await next.Invoke();
});

app.Map("/example", internalApp =>
    internalApp.Run(async context =>
    {
        System.Console.WriteLine("/example middleware triggered");
        await context.Response.WriteAsync("/example middleware triggered");

    }));
//***************************************
//app.MapWhen()
app.MapWhen(x => x.Request.Method == "GET", internalApp =>
{
    internalApp.Run(async (context) =>
    {
        System.Console.WriteLine("MapWhen Middleware tetiklendi.");
        context.Response.WriteAsync("/MapWhen middleware GET request kullanildigi icin tetiklendi.");
    });
});

//***************************************
//


app.MapControllers();
app.Run(); //this is a middleware but it causes a SHORT CIRCUIT, nothing that comes after the app.Run() middleware works.
