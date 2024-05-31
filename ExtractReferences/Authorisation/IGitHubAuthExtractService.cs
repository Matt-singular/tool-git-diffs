namespace ExtractReferences.Authorisation;
using Octokit;

public interface IGitHubAuthExtractService
{
  GitHubClient GetGitHubClient();
}