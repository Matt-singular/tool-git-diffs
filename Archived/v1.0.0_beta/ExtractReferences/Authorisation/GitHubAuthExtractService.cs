namespace ExtractReferences.Authorisation;

using Configuration.Settings;
using Microsoft.Extensions.Options;
using Octokit;

public class GitHubAuthExtractService(IOptions<SecretSettings> secretSettings) : IGitHubAuthExtractService
{
  private readonly SecretSettings SecretSettings = secretSettings.Value;

  /// <summary>
  /// Sets up the Octkit API client to access GitHub
  /// </summary>
  /// <returns>The authorised GitHub client</returns>
  public GitHubAuthorisationResponse GetGitHubClient()
  {
    var applicationName = "tool-git-diffs";
    var gitHubAccessToken = SecretSettings.GitHubAccessToken;

    var gitHubClient = new GitHubClient(new ProductHeaderValue(applicationName))
    {
      Credentials = new Credentials(gitHubAccessToken)
    };

    return new GitHubAuthorisationResponse
    {
      OrganisationName = SecretSettings.GitHubOrganisationName!,
      GitHubAuthClient = gitHubClient
    };
  }
}

public interface IGitHubAuthExtractService
{
  GitHubAuthorisationResponse GetGitHubClient();
}