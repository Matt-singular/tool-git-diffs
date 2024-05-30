namespace ApplicationConsole.Configuration;

using System.Collections.Generic;

/// <summary>
/// Represents the commit settings for the application as found in config.json
/// </summary>
public class CommitSettings
{
  public List<Rule> Rules { get; set; }
  public bool GroupByHeaders { get; set; }
  public bool IncludeUnmatchedCommits { get; set; }

  public class Rule
  {
    public string Header { get; set; }
    public string GroupBy { get; set; }
    public string Pattern { get; set; }
  }
}