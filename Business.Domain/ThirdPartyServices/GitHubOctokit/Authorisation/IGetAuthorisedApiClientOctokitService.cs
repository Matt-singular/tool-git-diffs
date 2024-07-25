namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;

/// <summary>
/// A service to get an authorised API client for the GitHub Octokit API.
/// </summary>
public interface IGetAuthorisedApiClientOctokitService
{
  public GetAuthorisedApiClientOctokitResponse CreateClient();
}