namespace Common.Shared.Extensions;

using Common.Shared.Config;
using Common.Shared.Services.Commits.GetCleanedCommits;
using Common.Shared.Services.Commits.GetRawCommits;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Common & shared startup extension methods
/// </summary>
public static class StartupExtensions
{
  /// <summary>
  /// Sets up the application configuration
  /// </summary>
  /// <param name="config"></param>
  /// <returns>The configuration builder with the appsettings and user secrets configured</returns>
  public static IConfigurationBuilder AddCommonSharedConfiguration(this IConfigurationBuilder config)
  {
    config
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddUserSecrets<Startup>(optional: true, reloadOnChange: true);

    return config;
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
    services.Configure<CommitSettings>(configuration.GetSection("Commits"));
    services.Configure<SecretSettings>(configuration.GetSection("Secrets"));

    // Add services here
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
    services.AddScoped<IGetRawCommitsDomainService, GetRawCommitsDomainService>();
    services.AddScoped<IGetCleanedCommitsDomainService, GetCleanedCommitsDomainService>();

    return services;
  }
}

public class Startup {/* Instantiate Dotnet secrets against this shared project */}