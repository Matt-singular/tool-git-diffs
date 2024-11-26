namespace Common.Shared.Models.Commits;

/// <summary>
/// Contains the cleaned commit details.
/// </summary>
public class CleanedCommitDetails
{
  /// <summary>
  /// The Jira references extracted from the commit's message
  /// </summary>
  public List<CommitReference> JiraReferences { get; internal set; } = null!;

  /// <summary>
  /// The raw commit details as pulled from GitHub
  /// </summary>
  public RawCommitDetails RawDetails { get; internal set; } = null!;
}