using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MeterGram.IoC.Functions;
using System;

[assembly: FunctionsStartup(typeof(MeterGram.ProjectSyncFunction.Startup))]
namespace MeterGram.ProjectSyncFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var executionContextOptions = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;
        var basePath = executionContextOptions.AppDirectory;

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appSettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appSettings.Development.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        builder.Services.AddFunctionDepenDencies(config);
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
