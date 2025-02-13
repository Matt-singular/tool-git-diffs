namespace TestHelpers;

using Microsoft.Extensions.DependencyInjection;
using TestHelpers.Mocks.Config;

/// <summary>
/// Some helper methods for common shared tests
/// </summary>
public static class CommonSharedTestsHelpers
{
  /// <summary>
  /// Registers the mocked instances of the configuration settings
  /// </summary>
  /// <param name="serviceCollection">An instance of the service collection</param>
  /// <returns>The service collection with the added configuration settings</returns>
  public static ServiceCollection MockConfigurationSettings(this ServiceCollection serviceCollection)
  {
    serviceCollection.AddSingleton(MockedCommitSettings.CreateEmptyOptions());
    serviceCollection.AddSingleton(MockedSecretSettings.CreateOptions());

    return serviceCollection;
  }
}