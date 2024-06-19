namespace ApplicationConsole.Base;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjectionExtension
{
  public static ServiceCollection AddApplicationConsoleServices(this ServiceCollection serviceCollection)
  {
    // Add the ApplicationConsole services
    serviceCollection.TryAddSingleton<IOrchestration, Orchestration>();

    return serviceCollection;
  }
}