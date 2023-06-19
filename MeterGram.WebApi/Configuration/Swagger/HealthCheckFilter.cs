﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using FitnessApp.Web.Api.Configuration.Options;

namespace MeterGram.WebApi.Configuration.Swagger;

public class HealthCheckFilter : IDocumentFilter
{
    private readonly HealthCheckOptions _options;
    public HealthCheckFilter(IConfiguration configuration)
    {
        _options = configuration.GetSection("HealthCheck").Get<HealthCheckOptions>();
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathItem = new OpenApiPathItem();
        var operation = new OpenApiOperation();
        operation.Summary = "Checks all dependencies for status";
        operation.Tags.Add(new OpenApiTag { Name = "HealthCheck" });
        var properties = new Dictionary<string, OpenApiSchema>
        {
            { "status", new OpenApiSchema() { Type = "string" } },
            { "errors", new OpenApiSchema() { Type = "array" } }
        };

        var response = new OpenApiResponse();
        response.Content.Add("application/json", new OpenApiMediaType
        {
            Schema = new OpenApiSchema
            {
                Type = "object",
                AdditionalPropertiesAllowed = true,
                Properties = properties,
            }
        });

        operation.Responses.Add("200", response);
        pathItem.AddOperation(OperationType.Get, operation);

        swaggerDoc?.Paths.Add(_options.Endpoint, pathItem);
    }
}
