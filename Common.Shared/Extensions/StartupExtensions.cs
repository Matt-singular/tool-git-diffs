namespace Common.Shared.Extensions;

using System.Diagnostics.CodeAnalysis;
using Common.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Common startup extension methods
/// </summary>
[ExcludeFromCodeCoverage]
public static class StartupExtensions
{
  /// <summary>
  /// Sets up the application configuration
  /// </summary>
  /// <param name="configuration">The configuration builder</param>
  /// <returns>The configuration builder with the appsettings and user secrets configured</returns>
  public static IConfigurationBuilder AddCommonSharedConfiguration(this IConfigurationBuilder configuration)
  {
    var assemblyLocation = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
    var appSettingsPath = Path.Combine(assemblyLocation!, "appsettings.Common.json");

    configuration
    .AddJsonFile(appSettingsPath, optional: true, reloadOnChange: true)
    .AddUserSecrets<Startup>(optional: true, reloadOnChange: true);

    return configuration;
  }

  /// <summary>
  /// Configures the various config setting models
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <param name="configuration">The application's configuration</param>
  /// <returns>The application's service collection with the setting objects configured</returns>
  public static IServiceCollection ConfigureCommonSettings(this IServiceCollection services, IConfiguration configuration)
  {
    // Configure strongly-typed settings objects

    return services;
  }

  /// <summary>
  /// Adds Common.Shared services to the service collection
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <returns>The service collection with Common.Shared services registered</returns>
  public static IServiceCollection AddCommonSharedServices(this IServiceCollection services)
  {
    // Register Common.Shared services

    return services;
  }
}