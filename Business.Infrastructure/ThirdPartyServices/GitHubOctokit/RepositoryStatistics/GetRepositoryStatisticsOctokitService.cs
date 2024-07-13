namespace Business.Infrastructure.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

using System.Threading.Tasks;
using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Common.Shared.Config;
using Microsoft.Extensions.Options;

/// <summary>
/// 
/// </summary>
public class GetRepositoryStatisticsOctokitService : IGetRepositoryStatisticsOctokitService
{
  private readonly SecretSettings secretSettings;
  private readonly GetAuthorisedApiClientOctokitResponse authorisedOctokitApiClient;

  public GetRepositoryStatisticsOctokitService(IOptions<SecretSettings> secretSettings,
    IGetAuthorisedApiClientOctokitService authorisedOctokitApiClient)
  {
    this.secretSettings = secretSettings.Value;
    this.authorisedOctokitApiClient = authorisedOctokitApiClient.Process();
  }

  public async Task<GetRepositoryStatisticsOctokitResponse> ProcessAsync(GetRepositoryStatisticsOctokitRequest request)
  {
    // Gets the repository statistics from Octokit
    var getRepoStatsOctokitTask = GetRepositoryStatisticsFromOctokit(request, secretSettings.GitHubOrganisationName);
    var repoStatsOctokitResponse = await getRepoStatsOctokitTask.ConfigureAwait(false);

    // Maps the Octokit response to the domain response
    var repositoryStatisticsResponse = new GetRepositoryStatisticsOctokitResponse
    {
      RepositoryName = request.RepositoryName,
      Commits = MapRepositoryCommitStatistics(repoStatsOctokitResponse)
    };

    return repositoryStatisticsResponse;
  }

  public async Task<Octokit.CompareResult> GetRepositoryStatisticsFromOctokit(GetRepositoryStatisticsOctokitRequest request, string? organisationName)
  {
    // Gets the repository statistics from Octokit
    var repoOctokitClient = this.authorisedOctokitApiClient.Repository.Commit;
    var repoStatsOctokitTask = repoOctokitClient.Compare(owner: organisationName, request.RepositoryName,
      @base: request.FromBranchOrTag, head: request.ToBranchOrTag);

    var repoStatsOctokitResponse = await repoStatsOctokitTask.ConfigureAwait(false);
    return repoStatsOctokitResponse;
  }

  private List<GetRepositoryStatisticsOctokitResponse.CommitDetails> MapRepositoryCommitStatistics(Octokit.CompareResult repoStatsOctokitResponse)
  {
    var commits = repoStatsOctokitResponse.Commits.Select(commit => new GetRepositoryStatisticsOctokitResponse.CommitDetails
    {
      Hash = commit.Sha,
      AuthorName = commit.Author.Login,
      AuthorEmail = commit.Commit.Author.Email,
      Message = commit.Commit.Message,
      DateOfCommit = commit.Commit.Author.Date.Date,
      IsMergeCommit = commit.CheckMergeCommit()
    });

    return commits.ToList();
  }
}