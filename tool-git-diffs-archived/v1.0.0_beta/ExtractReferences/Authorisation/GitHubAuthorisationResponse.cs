namespace ExtractReferences.Authorisation;

using Octokit;

public class GitHubAuthorisationResponse
{
  public GitHubClient GitHubAuthClient { get; set; }
  public string OrganisationName { get; set; }
}