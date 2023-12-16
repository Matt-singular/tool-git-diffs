namespace ReferenceExtraction.Models;

using System.Collections.Generic;

public class ExtractedReferences : Dictionary<string, (string header, List<string?> subReferences)>
{
  // TODO: what about group by headers?
}