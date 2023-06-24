using MeterGram.Core.Configuration;
using MeterGram.Infrastructure.Database.Configuration;
using MeterGram.Infrastructure.CourseService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeterGram.IoC.Functions;

public static class FunctionDependenciesConfiguration
{
    public static IServiceCollection AddFunctionDepenDencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddDatabaseServices(config);
        services.AddCourseService(config);
        services.AddMediatorServices().AddValidators(); 

        return services;
    }
}