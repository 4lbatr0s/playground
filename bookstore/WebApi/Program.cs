using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; //authentication icin gerekli
IWebHostEnvironment environment = builder.Environment;

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

/*
authentication'u jwtbearerin default authentication schemasına göre yap
Bu schemaya bir JwtBearer ata, öyle ki parametreleri asagidaki gibi olsun..
*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters{ //tokeni validate ederken kullanılacak parametereler.
        ValidateAudience = true, //kullancılar tokene sahip olabilsin.
        ValidateIssuer = true, //token saglayıcısı,
        ValidateLifetime = true, //token zamanı bitince erişime kapat.
        ValidateIssuerSigningKey = true, //tokeni sifrelerken kullanacagımız anahtar.
        ValidIssuer = configuration["Token:Issuer"],
        ValidAudience = configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero //Tokenin tüm zonelarda adil bir biçimde dağıtılabilmesi için kullanılır.
    };
});

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

app.UseAuthentication(); //Authenticate olmadan, Authorization mümkün değildir, o yüzden önce Authentication pipeline'a dahil edilmeli.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware(); //to catch the exceptions(errors)

app.MapControllers();

app.Run();
