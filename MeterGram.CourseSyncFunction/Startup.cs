using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MeterGram.IoC.Functions;
using System;
using System.Collections.Generic;

[assembly: FunctionsStartup(typeof(MeterGram.CourseSyncFunction.Startup))]
namespace MeterGram.CourseSyncFunction;

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

        var useAzureAppConfig = config.GetValue<Boolean>("FeatureFlags:UseAzureAppConfig");
        if (useAzureAppConfig)
        {
            var providers = new List<IConfigurationProvider>();
            providers.AddRange(config.Providers);

            string connectionString = config.GetConnectionString("AzureAppConfigConnection");
            var azureConfig = new ConfigurationBuilder()
                .AddAzureAppConfiguration(connectionString)
                .Build();
            providers.AddRange(azureConfig.Providers);

            config = new ConfigurationRoot(providers);
        }

        builder.Services.AddFunctionDepenDencies(config);
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
