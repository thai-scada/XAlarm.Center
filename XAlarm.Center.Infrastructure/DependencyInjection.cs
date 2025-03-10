using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Infrastructure.IdentityServer;
using XAlarm.Center.Infrastructure.IdentityServer.Abstractions;
using XAlarm.Center.Infrastructure.Seeds;

namespace XAlarm.Center.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            switch (appOptions.Database.Type)
            {
                default:
                    var databaseFolder = Path.Combine(AppContext.BaseDirectory, "..", "data");
                    if (!Directory.Exists(databaseFolder))
                        Directory.CreateDirectory(databaseFolder);
                    var connectionString = Path.Combine(databaseFolder, appOptions.Database.ConnectionString);
                    options.UseSqlite($"Data Source={connectionString}").UseSnakeCaseNamingConvention();
                    options.UseSeeding((context, _) =>
                    {
                        GlobalSettingSeed.Seed(context, GlobalSettingSeed.GetEntities());
                        ProjectSeed.Seed(context, ProjectSeed.GetEntities());
                    });
                    options.UseAsyncSeeding(async (context, _, cancellationToken) =>
                    {
                        await GlobalSettingSeed.SeedAsync(context, GlobalSettingSeed.GetEntities(),
                            cancellationToken);
                        await ProjectSeed.SeedAsync(context, ProjectSeed.GetEntities(), cancellationToken);
                    });
                    break;
            }
        });

        AddAuthentication(services);

        return services;
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<IUserProfileService, UserProfileService>();
        services.AddHttpClient<IJwtService, JwtService>();
        services.AddHttpClient<IJwtServiceExchange, JwtExchangeService>();
        services.AddHttpClient<IExecuteActionsEmailService, ExecuteActionsEmailService>();
    }
}