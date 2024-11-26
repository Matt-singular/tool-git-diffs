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

  public class Pattern
  {
    /// <summary>
    /// The regex to identify the commit reference
    /// </summary>
    public string? Regex { get; set; }

    /// <summary>
    /// The sorting priority for the specific commit reference
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// The header for the commit reference to group under
    /// </summary>
    public string? Header { get; set; }
  }
}