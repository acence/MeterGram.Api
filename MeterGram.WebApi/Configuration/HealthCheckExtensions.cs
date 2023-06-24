using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace MeterGram.WebApi.Configuration;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Options.HealthCheckOptions>(configuration.GetSection("HealthCheck"));
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("AppDatabaseConnection")!, name: "SqlServer")
            .AddUrlGroup(new Uri(new Uri(configuration.GetValue<string>("ProjectService:BaseUrl")!), "/swagger/index.html"), name: "ProjectService");

        return services;
    }

    public static WebApplication MapHealthCheck(this WebApplication app, IConfiguration configuration)
    {
        var healthCheckOptions = configuration.GetSection("HealthCheck").Get<Options.HealthCheckOptions>()!;
        app.MapHealthChecks(healthCheckOptions.Endpoint, new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
