namespace ConfigExtraction.Models;

using System;
using System.Collections.Generic;

/// <summary>
/// The model that the config.json will be deserialised into
/// </summary>
public class ConfigModel
{
  /// <summary>
  /// The global diff range to be applied to all repositories (unless overriden)
  /// </summary>
  public DiffRange? DiffRange { get; set; }

  /// <summary>
  /// The global commit options, this will alter how the commit references are structured
  /// </summary>
  public Commitoptions? CommitOptions { get; set; }

  /// <summary>
  /// The rules for the different references to extract and group
  /// </summary>
  public List<Reference> References { get; set; } = null!;

  /// <summary>
  /// The details for the repositories to generate diffs from.
  /// Includes optional properties that will override global values if present
  /// </summary>
  public List<Repository> Repositories { get; set; } = null!;
}

/// <summary>
/// The range for which the git diff will be generated.
/// These diffs will run against the specified repositories
/// </summary>
public class DiffRange
{
  /// <summary>
  /// The diffs will be generated from the specified branch or tag
  /// </summary>
  public DiffRangeValue? From { get; set; }

  /// <summary>
  /// The diffs will be generated up to the specified branch or tag
  /// </summary>
  public DiffRangeValue? To { get; set; }
}

/// <summary>
/// Whether we've specified a branch or tag to generate from.
/// We have opted to explicitly define a branch or tag as this alters how we generate the diffs without needing complex logic to identify tags.
/// The values are mutually exclusive (we expect EITHER a branch OR a tag)
/// </summary>
public class DiffRangeValue
{
  // The private properties required to use the custom getters and setters (to avoid recursively calling its own getter)
  private string? branch;
  private string? tag;

  /// <summary>
  /// The diff will run against a branch
  /// </summary>
  public string? Branch { get => this.branch; set => this.branch = ValidateMutualExclusivityRule(value, this.Tag, updatedValue: value); }

  /// <summary>
  /// The diff will run against a tag
  /// </summary>
  public string? Tag { get => this.tag; set => this.tag = ValidateMutualExclusivityRule(this.Branch, value, updatedValue: value); }

  /// <summary>
  /// Checks if the mutual exclusivity rule has been violated.
  /// This will happen if the user attempts to configure BOTH the branch AND the tag.
  /// </summary>
  /// <param name="branch">The Diff range value as a branch</param>
  /// <param name="tag">The Diff range value as a tag</param>
  /// <param name="updatedValue">The value we attempted to set</param>
  /// <returns>The value we attempted to set if the mutual exclusivity rule has not been violated</returns>
  /// <exception cref="InvalidOperationException"></exception>
  private static string? ValidateMutualExclusivityRule(string? branch, string? tag, string? updatedValue)
  {
    // You aren't allowed to set both the Branch and the Tag for an individual DiffRangeValue
    var mutualExclusiveRuleViolated = !string.IsNullOrEmpty(branch) && !string.IsNullOrEmpty(tag);

    if (mutualExclusiveRuleViolated)
    {
      // Short-circuit with a user friendly error message
      throw new InvalidOperationException("Cannot set both Branch and Tag simultaneously.");
    }

    return updatedValue;
  }
}

/// <summary>
/// The global commit options that customises how the commit references will be structured
/// </summary>
public class Commitoptions
{
  /// <summary>
  /// Whether to include commits that didn't have any valid references.
  /// If False, commits that have no valid references will be discarded.
  /// </summary>
  public bool CaptureCommitsWithoutReferences { get; set; }

  /// <summary>
  /// Allows different reference types to be grouped under user-provided headers.
  /// If False, the different reference types will not have any header.
  /// </summary>
  public bool GroupReferencesByHeader { get; set; }
}

/// <summary>
/// The rules that govern what references to extract from a commit messages.
/// The rule also allows for sub-references to be specified so that they may be grouped under a parent reference
/// </summary>
public class Reference
{
  /// <summary>
  /// The header for the reference (can be used in conjunction with the CommitOptions.GroupReferencesByHeader value)
  /// </summary>
  public string? Header { get; set; }

  /// <summary>
  /// The literal regex pattern that determines how to extract the reference.
  /// There will be some logic processing to ensure we handle some basic things like case sensitivity etc.
  /// </summary>
  public string Pattern { get; set; } = null!;

  /// <summary>
  /// A list of literal regex patterns that will be grouped with this reference.
  /// The grouping will be done on a commit message level, but there will be additional processing
  /// logic to ensure that a parent feature has all it's sub items in one place
  /// </summary>
  public List<string>? SubItems { get; set; }
}

/// <summary>
/// The details of the repository including the optional properties that will override global config entries
/// </summary>
public class Repository
{
  /// <summary>
  /// The "friendly name" for the repository
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// The user's full path to the repository
  /// </summary>
  public string Path { get; set; } = null!;

  /// <summary>
  /// Optional - override the global diff range if the repository-level branch or tag differs
  /// </summary>
  public DiffRange? DiffRange { get; set; }
}