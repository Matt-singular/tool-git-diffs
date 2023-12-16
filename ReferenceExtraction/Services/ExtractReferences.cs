namespace ReferenceExtraction.Services;

using System;
using System.Collections.Generic;
using ConfigExtraction.Models;
using ReferenceExtraction.Models;

/// <summary>
/// Extracts all of the ticket references from the list of commit messages
/// </summary>
public class ExtractReferences : IExtractReferences
{
  public ExtractedReferences Process(ReferencePatterns patterns, List<string> commits, ConfigModel config)
  {
    throw new NotImplementedException();
  }
}

public interface IExtractReferences
{
  public ExtractedReferences Process(ReferencePatterns patterns, List<string> commits, ConfigModel config);
}