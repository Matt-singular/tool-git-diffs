namespace ReferenceExtraction.Models;

using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ReferencePatterns : List<ReferencePatternValue>
{
}

public class ReferencePatternValue
{
  public string? Header { get; set; }
  public Regex Pattern { get; set; } = null!;
  public List<Regex> SubPatterns { get; set; } = null!;
}