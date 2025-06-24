using API.AuthenticationSchemas;
using API.Extensions;
using API.Middlewares;
using Application.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var loggerFactory = LoggerFactory.Create(static loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

var logger = loggerFactory.CreateLogger("Program");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "HeaderAuth";
    options.DefaultChallengeScheme = "HeaderAuth";
}).AddScheme<AuthenticationSchemeOptions, HeaderAuthenticationHandler>("HeaderAuth", null);

builder.Services.AddAuthorization();

builder.Services.AddApiServices(logger);
builder.Services.AddInfrastructureServices(builder.Configuration, logger);
builder.Services.AddApplicationServices(logger);

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();