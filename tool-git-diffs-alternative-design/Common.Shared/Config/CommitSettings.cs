namespace Common.Shared.Config;

/// <summary>
/// Represents the commit settings for the application as found in the appsettings.json file
/// </summary>
public class CommitSettings
{
  /// <summary>
  /// The different commit pattern details
  /// </summary>
  public Pattern[]? Patterns { get; set; }

  /// <summary>
  /// The sort order in terms of the different pattern headers
  /// </summary>
  public string[]? SortOrder { get; set; }

  public class Pattern(string regex, int? priority, string? header)
  {
    /// <summary>
    /// The regex to identify the commit reference
    /// </summary>
    public string? Regex { get; set; } = regex;

    /// <summary>
    /// The sorting priority for the specific commit reference
    /// </summary>
    public int? Priority { get; set; } = priority;

    /// <summary>
    /// The header for the commit reference to group under
    /// </summary>
    public string? Header { get; set; } = header;
  }
}