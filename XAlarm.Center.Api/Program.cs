using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting.WindowsServices;
using Scalar.AspNetCore;
using Serilog;
using XAlarm.Center.Api.Extensions;
using XAlarm.Center.Api.Options;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Infrastructure.IdentityServer;
using XAlarm.Center.Service;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : string.Empty
});

builder.Services.AddOpenApi();

if (OperatingSystem.IsWindows())
    builder.Host.UseWindowsService();

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
    loggerConfig.WriteTo.File(Path.Combine(AppContext.BaseDirectory, "..", "logs", "x-alarm-center-.log"),
        rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31);
});

builder.Configuration.AddOptionsConfiguration("app-options");
builder.Configuration.AddOptionsConfiguration("project-options");
builder.Configuration.AddIdentitySettingsConfiguration();

var appOptions = builder.Configuration.GetSection("AppOptions").Get<AppOptions>() ?? new AppOptions();
builder.Services.ConfigureOptions<AppOptionsSetup>();
builder.Services.ConfigureOptions<ProjectOptionsSetup>();
builder.Services.ConfigureOptions<IdentityOptionsSetup>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(x =>
{
    x.DocumentSettings = y =>
    {
        y.DocumentName = appOptions.AppName;
        y.Version = "v1";
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        configurePolicy => { configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();

builder.Services.AddInfrastructure(appOptions);
builder.Services.AddService(appOptions);

var app = builder.Build();

if (app.Environment.IsDevelopment() || appOptions.ForceApplyMigrations)
    app.ApplyMigrations();

app.UseDefaultExceptionHandler().UseFastEndpoints();

app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (app.Environment.IsProduction())
    builder.WebHost.UseUrls(appOptions.AppUrl.Url);

app.Run();