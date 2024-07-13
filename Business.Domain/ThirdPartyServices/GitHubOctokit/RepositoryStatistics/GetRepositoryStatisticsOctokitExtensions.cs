namespace Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public static class GetRepositoryStatisticsOctokitExtensions
{
  /// <summary>
  /// Checks if the particular commit was a merge or not
  /// </summary>
  /// <param name="commit">The Octokit GitHubCommit response</param>
  /// <returns>True if the commit in question was a merge commit, Otherwise False</returns>
  public static bool CheckMergeCommit(this Octokit.GitHubCommit commit)
  {
    var committerName = commit.Commit.Committer.Name;
    var gitHubCommitter = "GitHub";

    var isMergeCommit = committerName.Equals(gitHubCommitter);
    return isMergeCommit;
  }
}