namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;

using Octokit;
using Octokit.Internal;

/// <summary>
/// The authorised GitHub Octokit API client and encapsulated services
/// </summary>
public class GetAuthorisedApiClientOctokitResponse
{
  private readonly GitHubClient octokitApiClient;

  public GetAuthorisedApiClientOctokitResponse(string? accessToken) : base()
  {
    if (string.IsNullOrEmpty(accessToken))
    {
      throw new ArgumentNullException(nameof(accessToken), "GitHub access token has not been configured");
    }

    this.octokitApiClient = new GitHubClient(new ProductHeaderValue("tool-git-diffs"))
    {
      Credentials = new Credentials(accessToken)
    };
  }

  public async Task<Repository> GetRepositoryDataAsync(string? repositoryOwner, string repositoryName)
  {
    // Validate mandatory parameter values
    if (string.IsNullOrEmpty(repositoryOwner))
    {
      throw new ArgumentNullException(nameof(repositoryOwner), "Mandatory parameter has not been configured");
    }

    if (string.IsNullOrEmpty(repositoryName))
    {
      throw new ArgumentNullException(nameof(repositoryName), "Mandatory parameter has not been configured");
    }

    // Get the specified repository's data from Octokit
    var getRepositoryDataOctokitTask = this.octokitApiClient.Repository.Get(repositoryOwner, repositoryName);
    var getRepositoryDataOctokitResponse = await getRepositoryDataOctokitTask.ConfigureAwait(false);

    return getRepositoryDataOctokitResponse;
  }

  public async Task Test()
  {
    var repositoryApi1Client = this.octokitApiClient.Repository.Commit;
  }

  public async Task<IReadOnlyList<GitHubCommit>> GetRepositoryStatisticsAsync(long repositoryId, CommitRequest request)
  {
    // Vaslidate mandatory parameter values
    if (repositoryId == 0)
    {
      throw new ArgumentNullException(nameof(repositoryId), "Mandatory parameter has not been configured");
    }

    // Get the specified rrepository's statistics from Octokit
    var getRepositoryStatisticsOctokitTask = this.octokitApiClient.Repository.Commit.GetAll(repositoryId, request);
    var getRepositoryStatisticsOctokitResponse = await getRepositoryStatisticsOctokitTask.ConfigureAwait(false);

    return getRepositoryStatisticsOctokitResponse;
  }

  public async Task<CompareResult> GetRepositoryCommitsAsync(string? repositoryOwner, string repositoryName, string fromReference, string toReference)
  {
    // Validate mandatory parameter values
    if (string.IsNullOrEmpty(repositoryOwner))
    {
      throw new ArgumentNullException(nameof(repositoryOwner), "Mandatory parameter has not been configured");
    }

    // Get the specified rrepository's commits from Octokit
    var getRepositoryCommitsOctokitTask = this.octokitApiClient.Repository.Commit.Compare(owner: repositoryOwner, name: repositoryName,
      @base: toReference, head: fromReference);
    var getRepositoryCommitsOctokitResponse = await getRepositoryCommitsOctokitTask.ConfigureAwait(false);

    return getRepositoryCommitsOctokitResponse;
  }
}