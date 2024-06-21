namespace ExtractReferences.Dependency;

using ExtractReferences.Authorisation;
using ExtractReferences.Commits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// This class is responsible for setting up the services for ExtractReferences.
/// </summary>
public static class ExtractReferencesDependencyInjection
{
  /// <summary>
  /// Add the ExtractReferences services
  /// </summary>
  /// <param name="serviceCollection">The service collection to add the services to.</param>
  /// <returns>The configured ServiceCollection</returns>
  public static IServiceCollection AddExtractReferencesServices(this IServiceCollection serviceCollection)
  {
    // Add the Configuration services
    serviceCollection.TryAddSingleton<IGitHubAuthExtractService, GitHubAuthExtractService>();
    serviceCollection.TryAddSingleton<IPullCommitsExtractService, PullCommitsExtractService>();

    return serviceCollection;
  }
}