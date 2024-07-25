namespace Business.Infrastructure.Services.RepositoryStatistics;

using System.Threading.Tasks;
using Business.Domain.Services.RepositoryStatistics;
using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

/// <summary>
/// Fetches the list of commits for a specified organisation repository
/// </summary>
/// <param name="getOrgRepoCommitsOctokitService">Gets the list of commits for the specified organisation repository using the Octokit API</param>
public class GetOrgRepoRawCommitsDomainService(IGetOrgRepoCommitsOctokitService getOrgRepoCommitsOctokitService) : IGetOrgRepoRawCommitsDomainService
{
  public async Task<GetOrgRepoRawCommitsDomainResponse> GetRawCommits(GetOrgRepoRawCommitsDomainRequest domainRequest)
  {
    var getRepositoryStatisticsOctokitResponses = await GetRepositoryCommitsFromOctokitAsync(domainRequest).ConfigureAwait(false);

    var domainResponse = MapRepositoryCommits(getRepositoryStatisticsOctokitResponses);

    return domainResponse;
  }

  public async Task<List<GetOrgRepoCommitsOctokitDomainResponse>> GetRepositoryCommitsFromOctokitAsync(GetOrgRepoRawCommitsDomainRequest domainRequest)
  {
    var getOrgRepoCommitsOctokitTasks = domainRequest.Repositories.Select(repo =>
    {
      // Maps out the Octokit domain request
      var gitReferences = repo.GetGitReference(domainRequest);
      var getOrgRepoCommitsOctokitDomainRequest = new GetOrgRepoCommitsOctokitDomainRequest
      {
        RepositoryName = repo.RepositoryName,
        FromBranchOrTag = gitReferences.FromBranchOrTag,
        ToBranchOrTag = gitReferences.ToBranchOrTag,
        ExcludeMergeCommits = true
      };

      // Calls the Octokit API
      var getOrgRepoCommitsOctokitTask = getOrgRepoCommitsOctokitService.ProcessAsync(getOrgRepoCommitsOctokitDomainRequest);
      return getOrgRepoCommitsOctokitTask;
    }).ToList();

    // Wait for all of the Octokit API Tasks to complete
    var getOrgRepoCommitsOctokitResponses = await Task.WhenAll(getOrgRepoCommitsOctokitTasks).ConfigureAwait(false);
    return getOrgRepoCommitsOctokitResponses.ToList();
  }

  public static GetOrgRepoRawCommitsDomainResponse MapRepositoryCommits(List<GetOrgRepoCommitsOctokitDomainResponse> getRepositoryStatisticsOctokitResponses)
  {
    var repositoryCommits = getRepositoryStatisticsOctokitResponses
      .SelectMany(repo =>
      {
        var repoName = repo.RepositoryName;
        return repo.Commits.Select(repoCommits =>
        {
          var repositoryCommit = (GetOrgRepoRawCommitsDomainResponse.RepositoryCommits)repoCommits;
          repositoryCommit.RepositoryName = repoName;
          return repositoryCommit;
        });
      });

    var domainResponse = (GetOrgRepoRawCommitsDomainResponse)repositoryCommits.ToList();
    return domainResponse;
  }
}