namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;

using Octokit;

/// <summary>
/// The authorised GitHub Octokit API client
/// </summary>
public class GetAuthorisedApiClientOctokitResponse : GitHubClient
{
  public GetAuthorisedApiClientOctokitResponse(string? accessToken) : base(new ProductHeaderValue("tool-git-diffs"))
  {
    if (string.IsNullOrEmpty(accessToken))
    {
      throw new ArgumentNullException(nameof(accessToken), "GitHub access token has not been configured");
    }

    this.Credentials = new Credentials(accessToken);
  }
}