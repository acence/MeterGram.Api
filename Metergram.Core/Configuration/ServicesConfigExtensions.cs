using FluentValidation;
using MediatR;
using MeterGram.Core.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeterGram.Core.Configuration;

public static class ServicesConfigExtensions
{
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services
            .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        var typesToRegister = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace)
                && type.BaseType != null
                && type.BaseType.IsGenericType 
                && type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));

        foreach(var type in typesToRegister)
        {
            var validationInterface = type.GetInterfaces()
                .First(x => x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IValidator<>));

            services.AddTransient(validationInterface, type);
        }

        return services;
    }
}
