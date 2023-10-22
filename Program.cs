using AuthServerApp.Contexts;
using AuthServerApp.EventBus;
using AuthServerApp.EventBus.Abstract;
using AuthServerApp.Extensions;
using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using AuthServerApp.Repositories;
using AuthServerApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Plain.RabbitMQ;
using Prometheus;
using RabbitMQ.Client;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// My Service
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddSingleton(new ConnectionFactory()
{
    Uri = new Uri(builder.Configuration.GetSection("ConnectionStrings")["RabbitMQServerConnection"] ?? ""),
    DispatchConsumersAsync = true
});
builder.Services.AddSingleton(typeof(IRabbitMqProducer<>), typeof(ProducerBase<>));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["DataContext"]));
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(
        new ElasticsearchSinkOptions(new Uri(builder.Configuration.GetSection("ConnectionStrings")["ElasticSearchConnection"] ?? "http://localhost:9200"))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        }).WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddScoped<ISignIn, SignInService>();
builder.Services.AddScoped<ISignUp, SignUpService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();

var app = builder.Build();
app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleware();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(corsBuilder => corsBuilder.AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
