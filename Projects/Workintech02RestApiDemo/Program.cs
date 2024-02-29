using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using Workintech02RestApiDemo.Business;
using Workintech02RestApiDemo.Domain.Dto;
using Workintech02RestApiDemo.Domain.Helper;
using Workintech02RestApiDemo.Infrastructure;
using Workintech02RestApiDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db resolve and initialize



//add authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("SecretKey"))),
        };
    });

//automapper

builder.Services.AddAutoMapper(typeof(CityDtoProfile));

//httpClientHandler Resolver
builder.Services.AddTransient<IHttpClientHandler, Workintech02RestApiDemo.Domain.Helper.HttpClientHandler>();

#region Scrutor resolvers

var typeBaseService = typeof(BaseService);

var assembly = typeBaseService.Assembly;

builder.Services.Scan(selector =>
        selector
            .FromAssemblies(assembly)
            .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );



var singletonBaseAssembly = typeof(BaseSingletonService).Assembly;
builder.Services.Scan(selector =>
        selector
            .FromAssemblies(singletonBaseAssembly)
            .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseSingletonService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );

#endregion

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    


});

var app = builder.Build();


app.UseTimeElapsedCalculate();

//Debug-->Information-->Warning-->Error-->Fatal
#region SerilogConfiguration

var logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Information()
#endif
#if RELEASE
.MinimumLevel.Error()
#endif
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = logger;

#endregion


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request p
if (app.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<WorkintechBlogDemoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WorkintechBlogDemo")));
    builder.Services.AddDbContext<Workintech02CodeFirstContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WorkintechCodeFirstDemo")));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("WorkintechBlogDemo");
    builder.Services.AddDbContext<WorkintechBlogDemoContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("WorkintechBlogDemo"), ServerVersion.AutoDetect(connectionString)));

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCustomException();

DataSeeder.SeedCodeFirst(app);
DataSeeder.Seed(app);

app.Run();
