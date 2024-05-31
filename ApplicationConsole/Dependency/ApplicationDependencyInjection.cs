namespace ApplicationConsole.Dependency;

using Configuration;
using Configuration.Dependency;
using DiffGeneration.Dependency;
using ExtractReferences.Dependency;
using Microsoft.Extensions.DependencyInjection;
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
      services.AddConfigurationServices();
      services.AddExtractReferencesServices();
      services.AddDiffGenerationServices();
    });
  }

  /// <summary>
  /// Add the ApplicationConsole services
  /// </summary>
  /// <param name="serviceCollection">The service collection to add the services to.</param>
  /// <returns>The configured ServiceCollection</returns>
  public static IServiceCollection AddApplicationConsoleServices(this IServiceCollection serviceCollection)
  {
    // Add the ApplicationConsole services
    //serviceCollection.TryAddSingleton<IValidateConfigurationAppService, ValidateConfigurationAppService>();

    return serviceCollection;
  }

  /// <summary>
  /// Validates the application's configurations.
  /// </summary>
  /// <param name="serviceProvider">The service provider to use for retrieving services.</param>
  public static void ValidateConfigurations(this IServiceProvider serviceProvider)
  {
    var configuration = serviceProvider.GetRequiredService<IValidateConfigurationService>();
    configuration.Process();
  }
}