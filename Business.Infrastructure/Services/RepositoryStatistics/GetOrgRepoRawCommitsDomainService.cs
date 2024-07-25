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
  public Task<GetOrgRepoRawCommitsDomainResponse> GetRawCommits(GetOrgRepoRawCommitsDomainRequest domainRequest)
  {
    throw new NotImplementedException();
  }

  public async Task<List<GetOrgRepoCommitsOctokitDomainResponse>> GetRepositoryStatisticsFromOctokitAsync(GetOrgRepoRawCommitsDomainRequest domainRequest)
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
}