namespace Business.Infrastructure.Extensions;

using Business.Domain.Services.RepositoryStatistics;
using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;
using Business.Infrastructure.Services.RepositoryStatistics;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Statistics;
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
    // DomainServices
    services.TryAddScoped<IGetOrgRepoRawCommitsDomainService, GetOrgRepoRawCommitsDomainService>();

    // ThirdPartyServices - Octokit
    services.TryAddSingleton<IGetAuthorisedApiClientOctokitService, GetAuthorisedApiClientOctokitService>();
    services.TryAddScoped<IGetOrgRepoAuthorStatsOctokitService, GetOrgRepoAuthorStatsOctokitService>();
    services.TryAddScoped<IGetOrgRepoCommitsOctokitService, GetOrgRepoCommitsOctokitService>();

    return services;
  }
}