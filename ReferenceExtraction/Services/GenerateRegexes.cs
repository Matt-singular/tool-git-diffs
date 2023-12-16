namespace ReferenceExtraction.Services;

using System.Text.RegularExpressions;
using ConfigExtraction.Models;
using ReferenceExtraction.Models;

/// <summary>
/// Extracts all of the Regex patterns
/// </summary>
public class GenerateRegexes : IGenerateRegexes
{
  public ReferencePatterns Process(ConfigModel config)
  {
    // Check the config references patterns and create the regexes
    var configReferences = config.References;

    var referenceRegexEnumerable = configReferences.Select(reference => new ReferencePatternValue
    {
      Header = reference.Header,
      Pattern = new Regex(reference.Pattern),
      SubPatterns = reference?.SubItems?.Select(subPattern => new Regex(subPattern))?.ToList() ?? []
    });

    var referenceRegexes = referenceRegexEnumerable.ToList();

    // Set and return the responsee
    var response = new ReferencePatterns();
    response.AddRange(referenceRegexes);

    return response;
  }
}

public interface IGenerateRegexes
{
  public ReferencePatterns Process(ConfigModel config);
}