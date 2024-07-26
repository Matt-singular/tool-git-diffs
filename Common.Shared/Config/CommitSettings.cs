namespace Common.Shared.Config;

/// <summary>
/// Represents the commit settings for the application as found in the appsettings.json file
/// </summary>
public class CommitSettings
{
  public required List<CommitPattern> Patterns { get; set; }
  public required List<string> SortOrder { get; set; }
  public required string OrderBy { get; set; }

  public class CommitPattern
  {
    public required string Header { get; set; }
    public required string Pattern { get; set; }
    public bool? GroupBy { get; set; }
    public int? Priority { get; set; }
  }
}