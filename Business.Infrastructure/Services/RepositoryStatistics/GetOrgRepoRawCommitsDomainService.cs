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
    // Validate the Domain Request Model
    if (!domainRequest.ValidateModel())
    {
      throw new ArgumentNullException(nameof(domainRequest), "Manadatory data is missing from the request");
    }

    // Octokit API Calls
    var getRepositoryStatisticsOctokitTasks = GetRepositoryCommitsFromOctokitAsync(domainRequest);
    var getRepositoryStatisticsOctokitResponses = await getRepositoryStatisticsOctokitTasks.ConfigureAwait(false);

    // Mapping
    var repositoryCommits = MapRepositoryCommits(getRepositoryStatisticsOctokitResponses);
    var domainResponse = new GetOrgRepoRawCommitsDomainResponse();
    domainResponse.AddRange(repositoryCommits);

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

  public static List<GetOrgRepoRawCommitsDomainResponse.RepositoryCommits> MapRepositoryCommits(List<GetOrgRepoCommitsOctokitDomainResponse> getRepositoryStatisticsOctokitResponses)
  {
    var repositoryCommits = getRepositoryStatisticsOctokitResponses
      .SelectMany(repo =>
      {
        var repoName = repo.RepositoryName;
        return repo.Commits.Select(repoCommits => new GetOrgRepoRawCommitsDomainResponse.RepositoryCommits
        {
          RepositoryName = repoName,
          Hash = repoCommits.Hash,
          AuthorName = repoCommits.AuthorName,
          AuthorEmail = repoCommits.AuthorEmail,
          Message = repoCommits.Message,
          DateOfCommit = repoCommits.DateOfCommit,
          IsMergeCommit = repoCommits.IsMergeCommit,
        });
      });

    return repositoryCommits.ToList();
  }
}