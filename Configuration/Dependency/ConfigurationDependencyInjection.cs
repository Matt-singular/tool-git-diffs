namespace Configuration.Dependency;

using Configuration.ConfigurationHelpers;
using Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

/// <summary>
/// This class is responsible for setting up the services for configuration.
/// </summary>
public static class ConfigurationDependencyInjection
{
  /// <summary>
  /// Sets up the application configuration.
  /// </summary>
  /// <returns>The host builder with the configured application settings.</returns>
  public static IHostBuilder SetupApplicationConfiguration<TClass>(this IHostBuilder hostBuilder) where TClass : class
  {
    // Add config.json file
    var configPath = ConfigurationHelpers.GetConfigJsonPath();
    hostBuilder.ConfigureAppConfiguration((context, config) => config.AddJsonFile(configPath, optional: true, reloadOnChange: true));

    // Enable User Secrets
    hostBuilder.ConfigureAppConfiguration((context, config) => config.AddUserSecrets<TClass>());

    // Configure config.json settings
    hostBuilder.ConfigureServices((context, services) =>
    {
      // Configure strongly-typed settings objects
      services.Configure<SecretSettings>(context.Configuration.GetSection("Secrets"));
      services.Configure<FileSettings>(context.Configuration.GetSection("Files"));
      services.Configure<CommitSettings>(context.Configuration.GetSection("Commits"));
    });

    return hostBuilder;
  }

  /// <summary>
  /// Add the Configuration services
  /// </summary>
  /// <param name="serviceCollection">The service collection to add the services to.</param>
  /// <returns>The configured ServiceCollection</returns>
  public static IServiceCollection AddConfigurationServices(this IServiceCollection serviceCollection)
  {
    // Add the Configuration services
    serviceCollection.TryAddSingleton<IValidateConfigurationService, ValidateConfigurationService>();

    return serviceCollection;
  }
}