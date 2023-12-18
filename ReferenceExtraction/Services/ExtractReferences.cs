namespace ReferenceExtraction.Services;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConfigExtraction.Models;
using ReferenceExtraction.Models;

/// <summary>
/// Extracts all of the ticket references from the list of commit messages
/// </summary>
public class ExtractReferences : IExtractReferences
{
  public ExtractedReferences Process(ReferencePatterns patterns, List<string> commits, ConfigModel config)
  {
    // Flattened commit references
    var extractedCommits = ExtractAllCommitReferences(commits, patterns).SelectMany(x => x).ToList();

    return null!;
  }

  public IEnumerable<List<ReferenceEnumerable>> ExtractAllCommitReferences(List<string> commits, ReferencePatterns patterns)
  {
    foreach (var message in commits)
    {
      var extractedReferences = GetCommitReference(message, patterns).ToList();
      yield return extractedReferences;
    }
  }

  public IEnumerable<ReferenceEnumerable> GetCommitReference(string commitMessage, ReferencePatterns patterns)
  {
    // Normalise the commit message to simplify the pattern matching logic
    var normalisedMessage = commitMessage.Trim().ToUpper();

    // Run the commit message through each of the patterns
    foreach (var regex in patterns)
    {
      // Parent Pattern
      var parentRegex = regex.Pattern;
      var parentRefValues = GetPatternMatches(normalisedMessage, parentRegex);

      if (parentRefValues.hasMatches == false)
      {
        // Skip to next iteration if there aren't any matches
        continue;
      }

      // We have a parent reference, check for sub references
      var subPatterns = regex.SubPatterns ?? [];
      var subReferences = subPatterns.SelectMany(subPattern => GetPatternMatches(normalisedMessage, subPattern).matches).ToList();

      // Yield the current results
      var results = new ReferenceEnumerable
      {
        Header = regex.Header,
        ParentReferences = parentRefValues.matches,
        SubReferences = subReferences
      };
      yield return results;
    }
  }

  public (bool hasMatches, List<string> matches) GetPatternMatches(string message, Regex pattern)
  {
    // Get the extracted references for the specified message and regex
    var extractedReferences = pattern.Matches(message);
    var hasMatches = extractedReferences.Count > 0;

    if (hasMatches)
    {
      // Matches were found, get their string values
      var matches = extractedReferences.Select(reference => reference.Value).ToList();
      return (hasMatches, matches);
    }

    // No matches were found
    return (hasMatches, []);
  } 
}

public interface IExtractReferences
{
  public ExtractedReferences Process(ReferencePatterns patterns, List<string> commits, ConfigModel config);
}