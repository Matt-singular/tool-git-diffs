namespace Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Statistics;

using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;
using Common.Shared.Config;
using Microsoft.Extensions.Options;

/// <summary>
/// Gets some basic statistics for the specified author in the specified repository
/// </summary>
/// <param name="secretSettings">Contains the Organisation's name to run use when pulling repository statistics</param>
/// <param name="getAuthorisedApiClientOctokitService">The service that encapsulates the Octokit API client</param>
public class GetOrgRepoAuthorStatsOctokitService(IOptions<SecretSettings> secretSettings, IGetAuthorisedApiClientOctokitService getAuthorisedApiClientOctokitService) : IGetOrgRepoAuthorStatsOctokitService
{
  private readonly SecretSettings secretSettings = secretSettings.Value;
  private readonly GetAuthorisedApiClientOctokitResponse octokitApiClient = getAuthorisedApiClientOctokitService.Process();

  public async Task<GetOrgRepoAuthorStatsOctokitDomainResponse> ProcessAsync(GetOrgRepoAuthorStatsOctokitDomainRequest request)
  {
    // Secrets
    var organisation = secretSettings.GitHubOrganisationName;

    // Get repository details
    var getRepositoryDetailTask = this.octokitApiClient.Repository.Get(organisation, request.RepositoryName);
    var repositoryDetail = await getRepositoryDetailTask.ConfigureAwait(false);

    // Get repository statistics
    var repositoryId = repositoryDetail.Id;
    var getRepositoryStatsTask = this.octokitApiClient.Repository.Commit.GetAll(repositoryId, request);
    var repositoryStats = await getRepositoryStatsTask.ConfigureAwait(false);

    // Map the repository statistics per Author
    var authorStats = repositoryStats
      .GroupBy(repoStats => repoStats.Author.Login)
      .Select(authorStats => (authorName: authorStats.Key, commitCount: authorStats.Count()))
      .ToList();

    return new GetOrgRepoAuthorStatsOctokitDomainResponse
    {
      RepositoryName = repositoryDetail.Name,
      AuthorStatistics = authorStats
    };
  }
}