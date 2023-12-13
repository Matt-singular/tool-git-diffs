namespace ConfigExtraction.Base;

using ConfigExtraction.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjectionExtension
{
  public static ServiceCollection AddConfigExtractionServices(this ServiceCollection serviceCollection)
  {
    // Add the ConfigExtraction services
    serviceCollection.TryAddSingleton<IOrchestration, Orchestration>();
    serviceCollection.TryAddSingleton<IReadConfig, ReadConfig>();
    serviceCollection.TryAddScoped<IFileServices, FileServices>();

    return serviceCollection;
  }
}