using MeterGram.Infrastructure.Interfaces.ProjectService;
using MeterGram.Infrastructure.ProjectService.Options;
using MeterGram.Infrastructure.ProjectService.Services;
using MeterGram.Infrastructure.ProjectService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace MeterGram.Infrastructure.ProjectService.Configuration;

public static class ProjectServiceConfigExtensions
{
    public static IServiceCollection AddProjectService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ProjectServiceOptions>(configuration.GetSection("ProjectService"));
        services.AddMemoryCache();
        services.AddTransient<IProjectExternalService, ProjectExternalService>();

        services.AddHttpClient<ICourseHttpService, CourseHttpService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ProjectService:BaseUrl"]);
        })
        .AddPolicyHandler(GetRetryPolicy());

        return services;
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(delay);
    }
}
