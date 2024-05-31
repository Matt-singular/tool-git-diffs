namespace ExtractReferences.Commits;

using ExtractReferences.Authorisation;
using ExtractReferences.Extensions;

public class PullCommitsExtractService(IGitHubAuthExtractService gitHubAuthService) : IPullCommitsExtractService
{
  private readonly IGitHubAuthExtractService GitHubAuthService = gitHubAuthService;

  public async Task<GitHubCommitDetailsResponse> ProcessAsync(string repositoryName, string startRef, string endRef)
  {
    // Get GitHub client
    var gitHubAuthResponse = this.GitHubAuthService.GetGitHubClient();
    var gitHubClient = gitHubAuthResponse.GitHubAuthClient;
    var organisationName = gitHubAuthResponse.OrganisationName;
    var repository = gitHubClient.Repository.Commit;

    try
    {
      // Pulling commits for the GitHub repository
      var repositoryCommitsTask = repository.Compare(owner: organisationName, repositoryName, @base: startRef, head: endRef);
      var repositoryCommitsResponse = await repositoryCommitsTask.ConfigureAwait(false);

      // Mapping GitHub commits
      var gitHubCommitDetailsResponse = new GitHubCommitDetailsResponse
      {
        OrganisationName = organisationName,
        RepositoryName = repositoryName,
        CommitDetails = MapGitHubCommitDetails(repositoryCommitsResponse.Commits)
      };
      return gitHubCommitDetailsResponse;
    }
    catch (Octokit.NotFoundException ex)
    {
      // Handle errors from Octokit
      var message = $"The repository '{repositoryName}', startRef '{startRef}' and/or endRef '{endRef}' do not exist";
      throw new Octokit.NotFoundException(message, System.Net.HttpStatusCode.InternalServerError);
    }
    catch (Exception ex)
    {
      var message = $"Unknown error occured, unable to pull commits for Repository '{repositoryName}'";
      throw new Exception(message);
    }
  }

  public static List<GitHubCommitDetailsResponse.Commits> MapGitHubCommitDetails(IReadOnlyList<Octokit.GitHubCommit> commits)
  {
    var mappedCommits = new List<GitHubCommitDetailsResponse.Commits>();

    foreach (var commit in commits)
    {
      mappedCommits.Add(new GitHubCommitDetailsResponse.Commits
      {
        Hash = commit.Sha,
        AuthorName = commit.Author.Login,
        AuthorEmail = commit.Commit.Author.Email,
        Message = commit.Commit.Message,
        DateOfCommit = commit.Commit.Author.Date.Date,
        IsMergeCommit = commit.CheckMergeCommit()
      });
    }

    return mappedCommits;
  }
}

public interface IPullCommitsExtractService
{
  Task<GitHubCommitDetailsResponse> ProcessAsync(string repositoryName, string startRef, string endRef);
}