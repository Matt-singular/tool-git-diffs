namespace ExtractReferences.Extensions;
using Octokit;

public static class OctokitExtensions
{
  /// <summary>
  /// Checks if the particular commit was a merge or not
  /// </summary>
  /// <param name="commit">The Octokit GitHubCommit response</param>
  /// <returns>True if the commit in question was a merge commit, Otherwise False</returns>
  public static bool CheckMergeCommit(this GitHubCommit commit)
  {
    var committer = commit.Commit.Committer.Name;
    var gitHubCommitter = "GitHub";
    var isMergeCommit = committer.Equals(gitHubCommitter);

    return isMergeCommit;
  }
}