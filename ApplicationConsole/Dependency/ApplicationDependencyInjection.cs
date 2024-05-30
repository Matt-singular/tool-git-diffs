namespace ApplicationConsole.Dependency;

using ApplicationConsole.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

/// <summary>
/// This class is responsible for setting up the services for the application.
/// </summary>
public static class ApplicationDependencyInjection
{
  /// <summary>
  /// Sets up the project services and builds the service provider.
  /// </summary>
  /// <returns>The host builder with all the configured services.</returns>
  public static IHostBuilder SetupProjectServices(this IHostBuilder hostBuilder)
  {
    return hostBuilder.ConfigureServices((context, services) =>
    {
      services.AddApplicationConsoleServices();
    });
  }

  /// <summary>
  /// Sets up the application configuration.
  /// </summary>
  /// <returns>The host builder with the configured application settings.</returns>
  public static IHostBuilder SetupApplicationConfiguration(this IHostBuilder hostBuilder)
  {
    // Add config.json file
    hostBuilder.ConfigureAppConfiguration((context, config) => config.AddJsonFile("config.json", optional: true, reloadOnChange: true));
    hostBuilder.ConfigureAppConfiguration((context, config) => config.AddUserSecrets<Program>());

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
  /// Add the ApplicationConsole services
  /// </summary>
  /// <param name="serviceCollection">The service collection to add the services to.</param>
  /// <returns>The configured ServiceCollection</returns>
  private static IServiceCollection AddApplicationConsoleServices(this IServiceCollection serviceCollection)
  {
    // Add the ApplicationConsole services
    serviceCollection.TryAddSingleton<IConfigurationAppService, ConfigurationAppService>();
    serviceCollection.TryAddSingleton<IOrchestrationAppConsole, OrchestrationAppService>();

    return serviceCollection;
  }

  /// <summary>
  /// Validates the application's configurations.
  /// </summary>
  /// <param name="serviceProvider">The service provider to use for retrieving services.</param>
  public static void ValidateConfigurations(this IServiceProvider serviceProvider)
  {
    var configuration = serviceProvider.GetRequiredService<IConfigurationAppService>();
    configuration.Process();
  }

  /// <summary>
  /// Executes the diff generation process.
  /// </summary>
  /// <param name="serviceProvider">The service provider to use for the diff generation.</param>
  /// <param name="build">The build to use for the diff generation.</param>
  /// <param name="from">The starting point for the diff generation.</param>
  /// <param name="to">The ending point for the diff generation.</param>
  public static void ExecuteDiffGeneration(this IServiceProvider serviceProvider, string build, string from, string to)
  {
    var orchestration = serviceProvider.GetRequiredService<IOrchestrationAppConsole>();
    orchestration.Process();
  }
}