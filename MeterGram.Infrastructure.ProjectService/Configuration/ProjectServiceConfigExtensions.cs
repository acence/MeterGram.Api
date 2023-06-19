using MeterGram.Infrastructure.Interfaces.ProjectService;
using MeterGram.Infrastructure.ProjectService.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeterGram.Infrastructure.ProjectService.Configuration;

public static class ProjectServiceConfigExtensions
{
    public static IServiceCollection AddProjectService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ProjectServiceOptions>(configuration.GetSection("ProjectService"));
        services.AddMemoryCache();
        services.AddTransient<IProjectExternalService, ProjectExternalService>();

        return services;
    }
}
