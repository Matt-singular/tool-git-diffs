namespace ConfigExtraction.Base;

using ConfigExtraction.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjectionExtension
{
  /// <summary>
  /// Add the ConfigExtraction services
  /// </summary>
  /// <param name="serviceCollection"></param>
  /// <returns>The configured ServiceCollection</returns>
  public static ServiceCollection AddConfigExtractionServices(this ServiceCollection serviceCollection)
  {
    // Singleton Services
    serviceCollection.TryAddSingleton<IOrchestration, Orchestration>();
    serviceCollection.TryAddSingleton<IReadConfig, ReadConfig>();

    // Scoped Services
    serviceCollection.TryAddScoped<IFileServices, FileServices>();
    serviceCollection.TryAddScoped<IValidateConfig, ValidateConfig>();

    return serviceCollection;
  }
}