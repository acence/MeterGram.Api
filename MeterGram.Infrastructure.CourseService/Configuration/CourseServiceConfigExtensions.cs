using MeterGram.Infrastructure.CourseService.Options;
using MeterGram.Infrastructure.CourseService.Services;
using MeterGram.Infrastructure.CourseService.Services.Interfaces;
using MeterGram.Infrastructure.Interfaces.CourseService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace MeterGram.Infrastructure.CourseService.Configuration;

public static class CourseServiceConfigExtensions
{
    public static IServiceCollection AddCourseService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CourseServiceOptions>(configuration.GetSection("CourseService"));
        services.AddMemoryCache();
        services.AddTransient<ICourseExternalService, CourseExternalService>();

        services.AddHttpClient<ICourseHttpService, CourseHttpService>(client =>
        {
            client.BaseAddress = new Uri(configuration["CourseService:BaseUrl"]);
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
