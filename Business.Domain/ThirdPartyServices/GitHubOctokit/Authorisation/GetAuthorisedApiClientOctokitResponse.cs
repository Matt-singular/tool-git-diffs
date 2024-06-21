namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;

using Octokit;

public class GetAuthorisedApiClientOctokitResponse : GitHubClient
{
  public GetAuthorisedApiClientOctokitResponse(string accessToken) : base(new ProductHeaderValue("tool-git-diffs"))
  {
    this.Credentials = new Credentials(accessToken);
  }
}