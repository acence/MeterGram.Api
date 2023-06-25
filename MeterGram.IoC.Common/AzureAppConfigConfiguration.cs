using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace MeterGram.IoC.Common;

public static class AzureAppConfigConfiguration
{
    public static ConfigurationManager AddAzureAppConfig(this ConfigurationManager configuration)
    {
        var useAzureAppConfig = configuration.GetValue<Boolean>("FeatureFlags:UseAzureAppConfig");
        if (useAzureAppConfig)
        {
            string connectionString = configuration.GetConnectionString("AzureAppConfigConnection");
            configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString)
                    .ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeSharedTokenCacheCredential = true }));
                    });
            });
        }

        return configuration;
    }
}