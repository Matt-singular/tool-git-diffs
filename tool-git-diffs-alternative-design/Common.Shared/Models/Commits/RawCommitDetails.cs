namespace Common.Shared.Models.Commits;

/// <summary>
/// Contains the raw commit details
/// </summary>
public class RawCommitDetails
{
  /// <summary>
  /// The commit's unique hash
  /// </summary>
  public string Hash { get; internal set; } = null!;

  /// <summary>
  /// The author of the commit
  /// </summary>
  public string AuthorName { get; internal set; } = null!;

  /// <summary>
  /// The email (possibly obfuscated) of the commit author
  /// </summary>
  public string AuthorEmail { get; internal set; } = null!;

  /// <summary>
  /// The message associated with the commit
  /// </summary>
  public string Message { get; internal set; } = null!;

  /// <summary>
  /// Whether it is a merge commit
  /// </summary>
  public bool IsMergeCommit { get; internal set; }

  /// <summary>
  /// The Date/Time the commit was made
  /// </summary>
  public DateTime DateOfCommit { get; internal set; }
}