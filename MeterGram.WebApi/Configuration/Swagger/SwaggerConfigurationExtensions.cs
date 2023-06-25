using MeterGram.WebApi.Contracts.Responses;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MeterGram.WebApi.Configuration.Swagger;

public static class SwaggerConfigurationExtensions
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            var xmlContractsFilename = $"{Assembly.GetAssembly(typeof(ServerErrorResponse)).GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlContractsFilename));
        });
        return services;
    }

    public static WebApplication UseConfiguredSwagger(this WebApplication app)
    {
        var descriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in descriptionProvider.ApiVersionDescriptions.Select(x => x.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{description}/swagger.json", $"MeterGram Course WebApi {description}");
            }
        });

        return app;
    }
}
