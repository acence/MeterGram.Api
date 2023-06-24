using MeterGram.Core.Configuration;
using MeterGram.Infrastructure.Database.Configuration;
using MeterGram.Infrastructure.ProjectService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeterGram.IoC.Functions;

public static class FunctionDependenciesConfiguration
{
    public static IServiceCollection AddFunctionDepenDencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddDatabaseServices(config);
        services.AddProjectService(config);
        services.AddMediatorServices().AddValidators(); 

        return services;
    }
}