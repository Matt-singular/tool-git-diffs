namespace ReferenceExtraction.Services;

using System;
using System.Collections.Generic;
using ReferenceExtraction.Models;

public class ExtractReferences : IExtractReferences
{
  public ExtractedReferences Process(ReferencePatterns patterns, List<string> commits)
  {
    throw new NotImplementedException();
  }
}

public interface IExtractReferences
{
  public ExtractedReferences Process(ReferencePatterns patterns, List<string> commits);
}