namespace ReferenceExtraction.Models;

using System.Collections.Generic;

public class ExtractedReferences : Dictionary<string, (string header, List<string?> subReferences)>
{
  // TODO: what about group by headers?
}

public class ReferenceEnumerable
{
  public string? Header { get; set; }
  public List<string>? ParentReferences { get; set; } // TODO: what if they specify two parent references in one message?  how would we group these? For now we'll just grab everything
  public List<string>? SubReferences { get; set; }
}