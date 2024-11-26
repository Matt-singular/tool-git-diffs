namespace Common.Shared.Models.Commits;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The commit reference details as processed and pulled from 
/// </summary>
public class CommitReference
{
  /// <summary>
  /// The Jira reference extracted from the commit message
  /// </summary>
  public string JiraReference { get; internal set; } = null!;

  /// <summary>
  /// The grouping priority for the Jira reference where high priority numbers are prioritised over lower priority numbers
  /// </summary>
  public int Priority { get; internal set; } = default;
}