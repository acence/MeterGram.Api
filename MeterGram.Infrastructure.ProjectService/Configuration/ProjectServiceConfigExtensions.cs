using Microsoft.Extensions.DependencyInjection;

namespace MeterGram.Infrastructure.ProjectService.Configuration
{
    public static class ProjectServiceConfigExtensions
    {
        public static IServiceCollection AddProjectService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }
    }
}