namespace Common.Shared.Extensions;

using Common.Shared.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class StartupExtensions
{
  /// <summary>
  /// Sets up the application configuration.
  /// </summary>
  /// <param name="config"></param>
  /// <returns>The configuration builder with the appsettings and user secrets configured.</returns>
  public static IConfigurationBuilder SetupCommonSharedConfiguration(this IConfigurationBuilder config)
  {
    config
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddUserSecrets<Startup>();

    return config;
  }

  /// <summary>
  /// Configures the various config setting models
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <param name="configuration">The application's configuration</param>
  /// <returns>The application's service collection with the setting objects configured</returns>
  public static IServiceCollection SetupCommonSharedConfigSettings(this IServiceCollection services, IConfiguration configuration)
  {
    // Configure strongly-typed settings objects
    services.Configure<SecretSettings>(configuration.GetSection("Secrets"));
    services.Configure<CommitSettings>(configuration.GetSection("Commits"));

    // Add services here
    return services;
  }

  /// <summary>
  /// Configures the Common.Shared services
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <returns>The configured services</returns>
  public static IServiceCollection AddCommonSharedServices(this IServiceCollection services)
  {
    // Register Common.Shared services

    return services;
  }
}

public class Startup {/* Instantiate Dotnet secrets against this shared project */}