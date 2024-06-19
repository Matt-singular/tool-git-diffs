namespace ReferenceExtraction;

using ConfigExtraction.Models;
using ReferenceExtraction.Services;

/// <summary>
/// Orchestration of all the reference extraction related logic
/// </summary>
public class Orchestration(IGenerateRegexes generateRegexes, IExtractReferences extractReferences) : IOrchestration
{
  public void Process(List<string> commitMessages, ConfigModel config)
  {
    // 1) Extract the Regex patterns from the config
    var regexPatterns = generateRegexes.Process(config);

    // 2) Extract the actual references
    var references = extractReferences.Process(regexPatterns, commitMessages, config);
  }
}

public interface IOrchestration
{
  public void Process(List<string> commitMessages, ConfigModel config);
}