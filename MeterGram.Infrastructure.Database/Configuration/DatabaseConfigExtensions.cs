﻿using MeterGram.Infrastructure.Database.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeterGram.Infrastructure.Database.Configuration;

public static class DatabaseConfigExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppDatabaseConnection");

        services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });


        var typesToRegister = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace)
                && type.BaseType != null
                && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));

        foreach (var type in typesToRegister)
        {

            var implementedInterface = type.GetInterfaces()
                .First(x => 
                x.GetInterfaces().Any(y => 
                y.GetGenericTypeDefinition() == typeof(IBaseRepository<>)));

            services.AddTransient(implementedInterface, type);
        }

        return services;
    }
}
