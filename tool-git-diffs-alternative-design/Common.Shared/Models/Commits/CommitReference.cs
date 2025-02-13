namespace Common.Shared.Models.Commits;
/// <summary>
/// The commit reference details as processed and pulled from 
/// </summary>
public class CommitReference
{
  /// <summary>
  /// The Jira reference extracted from the commit message
  /// </summary>
  public string JiraReference { get; set; } = null!;

  /// <summary>
  /// The sorting priority for the Jira reference (lower numbers are prioritised over higher ones)
  /// </summary>
  public int? Priority { get; set; }
}