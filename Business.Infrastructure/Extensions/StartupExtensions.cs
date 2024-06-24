namespace Business.Infrastructure.Extensions;

using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class StartupExtensions
{
  /// <summary>
  /// Configures the Business.Infrastructure services
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <returns>The configured services</returns>
  public static IServiceCollection AddBusinessInfrastructureServices(this IServiceCollection services)
  {
    // ThirdPartyServices - GitHubOctokit
    services.TryAddSingleton<IGetAuthorisedApiClientOctokitService, GetAuthorisedApiClientOctokitService>();

    return services;
  }
}