namespace DiffGeneration.Dependency;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This class is responsible for setting up the services for ExtractReferences.
/// </summary>
public static class DiffGenerationDependencyInjection
{
  /// <summary>
  /// Add the DiffGeneration services
  /// </summary>
  /// <param name="serviceCollection">The service collection to add the services to.</param>
  /// <returns>The configured ServiceCollection</returns>
  public static IServiceCollection AddDiffGenerationServices(this IServiceCollection serviceCollection)
  {
    // Add the Configuration services
    //serviceCollection.TryAddSingleton<IGitHubAuthExtractService, GitHubAuthExtractService>();

    return serviceCollection;
  }
}