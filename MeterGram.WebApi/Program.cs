using MeterGram.IoC.Common;
using MeterGram.IoC.WebApi;
using MeterGram.WebApi.Configuration;
using MeterGram.WebApi.Configuration.Swagger;
using MeterGram.WebApi.Contracts.Responses;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfig();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddWebApiDependencies(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ServerErrorResponse)));

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddConfiguredSwagger();
builder.Services.AddHealthCheck(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseConfiguredSwagger();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthCheck(builder.Configuration);

app.Run();

// Used for integration tests
public partial class Program
{
    protected Program()
    {
    }
}
