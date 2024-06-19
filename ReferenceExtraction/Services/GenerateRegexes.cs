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

    var referenceRegexesEnumerable = configReferences.Select(reference =>
    {
      // Grab the sub item references regex patterns
      var subPatternsEnumerable = reference?.SubItems?.Select(subPattern => new Regex(subPattern));
      var subPatterns = subPatternsEnumerable?.ToList() ?? [];

      return new ReferencePatternValue
      {
        Header = reference!.Header,
        Pattern = new Regex(reference.Pattern),
        SubPatterns = subPatterns
      };
    });
    var referenceRegexes = referenceRegexesEnumerable.ToList();

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