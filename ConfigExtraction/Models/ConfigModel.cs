namespace ConfigExtraction.Models;

using System.Collections.Generic;

public class ConfigModel
{
  public DiffRange? DiffRange { get; set; }
  public Commitoptions? CommitOptions { get; set; }
  public List<Reference> References { get; set; } = null!;
  public List<Repository> Repositories { get; set; } = null!;
}

public class DiffRange
{
  public DiffRangeValue? From { get; set; }
  public DiffRangeValue? To { get; set; }
}

public class DiffRangeValue
{
  public string Branch { get; set; }
  public string Tag { get; set; }
}

public class Commitoptions
{
  public bool CaptureCommitsWithoutReferences { get; set; }
  public bool GroupReferencesByHeader { get; set; }
}

public class Reference
{
  public string? Header { get; set; }
  public string Pattern { get; set; } = null!;
  public List<string>? SubItems { get; set; }
}

public class Repository
{
  public string? Name { get; set; }
  public string Path { get; set; } = null!;
  public DiffRange? DiffRange { get; set; }
}