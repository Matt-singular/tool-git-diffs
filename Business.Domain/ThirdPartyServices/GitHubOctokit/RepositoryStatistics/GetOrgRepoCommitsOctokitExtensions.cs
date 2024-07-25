namespace Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

using System.Text.RegularExpressions;

public static partial class GetRepositoryStatisticsOctokitExtensions
{
  [GeneratedRegex(@"^(?:Merge branch\s+)?'([^']+)' of (https:\/\/)?github\.com(\/)?(:)?[^\/]+\/[^\/]+ into ([^\s]+)$")]
  private static partial Regex CommitMergeMessageRegex();

  /// <summary>
  /// Checks if the particular commit was a merge or not
  /// </summary>
  /// <param name="commit">The Octokit GitHubCommit response</param>
  /// <returns>True if the commit in question was a merge commit, Otherwise False</returns>
  public static bool CheckMergeCommit(this Octokit.GitHubCommit commit)
  {
    // Merge Commit was done via GitHub
    var committerName = commit.Commit.Committer.Name;
    var gitHubCommitter = "GitHub";

    var isGitHubMergeCommit = committerName.Equals(gitHubCommitter);
    if (isGitHubMergeCommit)
    {
      // We already know it's a merge commit, so we short-circuit
      return true;
    }

    // Merge Commit was done explicitly by a particular user
    var commitMessage = commit.Commit.Message.Trim();
    var isMergeCommitMessage = CommitMergeMessageRegex().IsMatch(commitMessage);

    return isMergeCommitMessage;
  }
}