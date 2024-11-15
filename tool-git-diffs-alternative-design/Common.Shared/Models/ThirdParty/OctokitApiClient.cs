namespace Common.Shared.Models.ThirdParty;

using Octokit;

/// <summary>
/// Contains an authorised Octokit client for interacting with the GitHub API
/// </summary>
public class OctokitApiClient : GitHubClient
{
  public OctokitApiClient(string? accessToken) : base(new ProductHeaderValue("tool-git-diffs"))
  {
    if (string.IsNullOrEmpty(accessToken))
    {
      throw new ArgumentNullException(nameof(accessToken), "GitHub access token has not been configured");
    }

    Credentials = new Credentials(accessToken);
  }
}