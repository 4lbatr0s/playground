using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//database.
builder.Services.AddDbContext<BookStoreDBContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
//IBookStoreDBContext instance'i bir request baslatildiginda olustuurlsun ve request bittiğinde yok olsun.
//provider => provider.GetService.. sana bir provider veriyorum ve o provider da BookStoreDBContext, yani bunun ornegini uret.
builder.Services.AddScoped<IBookStoreDBContext>(provider => provider.GetService<BookStoreDBContext>()); 
//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//DI Container Services
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

//application starts here.
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware(); //to catch the exceptions(errors)

app.MapControllers();

app.Run();
