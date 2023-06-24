using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Metergram.Core.Configuration;
using MeterGram.Infrastructure.Database.Configuration;
using MeterGram.Infrastructure.ProjectService.Configuration;

namespace MeterGram.IoC.WebApi
{
    public static class WebApiDependenciesConfiguration
    {
        public static IServiceCollection AddWebApiDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDatabaseServices(config);
            services.AddProjectService(config);
            services.AddMediatorServices().AddValidators();

            return services;
        }
    }
}