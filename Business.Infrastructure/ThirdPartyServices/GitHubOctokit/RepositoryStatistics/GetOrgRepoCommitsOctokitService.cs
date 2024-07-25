namespace Business.Infrastructure.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

using System.Threading.Tasks;
using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Common.Shared.Config;
using Microsoft.Extensions.Options;

/// <summary>
/// Gets the list of commits for the specified organisation repository
/// </summary>
/// <param name="secretSettings">Contains the Organisation's name to run use when pulling repository statistics</param>
/// <param name="getAuthorisedApiClientOctokitService">The service that encapsulates the Octokit API client</param>
public class GetOrgRepoCommitsOctokitService(IOptions<SecretSettings> secretSettings, IGetAuthorisedApiClientOctokitService getAuthorisedApiClientOctokitService) : IGetOrgRepoCommitsOctokitService
{
  private readonly SecretSettings secretSettings = secretSettings.Value;
  private readonly GetAuthorisedApiClientOctokitResponse octokitApiClient = getAuthorisedApiClientOctokitService.CreateClient();

  public async Task<GetOrgRepoCommitsOctokitDomainResponse> ProcessAsync(GetOrgRepoCommitsOctokitDomainRequest request)
  {
    // Gets the repository statistics from Octokit
    var getRepoStatsOctokitTask = GetRepositoryStatisticsFromOctokit(request, secretSettings.GitHubOrganisationName);
    var repoStatsOctokitResponse = await getRepoStatsOctokitTask.ConfigureAwait(false);

    // Maps the Octokit response to the domain response
    var repositoryStatisticsResponse = new GetOrgRepoCommitsOctokitDomainResponse
    {
      RepositoryName = request.RepositoryName,
      Commits = MapRepositoryCommitStatistics(repoStatsOctokitResponse, request.ExcludeMergeCommits)
    };

    return repositoryStatisticsResponse;
  }

  public async Task<Octokit.CompareResult> GetRepositoryStatisticsFromOctokit(GetOrgRepoCommitsOctokitDomainRequest request, string? organisationName)
  {
    // Gets the repository statistics from Octokit
    var getRepositoryCommitsOctokitTask = this.octokitApiClient.GetRepositoryCommitsAsync(repositoryOwner: organisationName, request.RepositoryName, request.FromBranchOrTag, request.ToBranchOrTag);
    var getRepositoryCommitsOctokitResponse = await getRepositoryCommitsOctokitTask.ConfigureAwait(false);

    return getRepositoryCommitsOctokitResponse;
  }

  private static List<GetOrgRepoCommitsOctokitDomainResponse.CommitDetails> MapRepositoryCommitStatistics(Octokit.CompareResult repoStatsOctokitResponse, bool excludeMergeCommits)
  {
    // Maps the Octokit commits into a custom domain response
    var commits = repoStatsOctokitResponse.Commits.Select(commit => new GetOrgRepoCommitsOctokitDomainResponse.CommitDetails
    {
      Hash = commit.Sha,
      AuthorName = commit.Author.Login,
      AuthorEmail = commit.Commit.Author.Email,
      Message = commit.Commit.Message,
      DateOfCommit = commit.Commit.Author.Date.Date,
      IsMergeCommit = commit.CheckMergeCommit()
    });

    if (excludeMergeCommits)
    {
      // Filter out all the merge commits
      commits = commits.Where(commit => !commit.IsMergeCommit);
    }

    return commits.ToList();
  }
}