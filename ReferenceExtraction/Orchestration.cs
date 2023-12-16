namespace ReferenceExtraction;

using ConfigExtraction.Models;
using ReferenceExtraction.Services;

/// <summary>
/// Orchestration of all the reference extraction related logic
/// </summary>
public class Orchestration(IGenerateRegexes generateRegexes) : IOrchestration
{
  public void Process(List<string> commitMessaages, ConfigModel config)
  {
    // 1) Extract the Regex patterns from the config
    var regexPatterns = generateRegexes.Process(config);
  }
}

public interface IOrchestration
{
  public void Process(List<string> commitMessaages, ConfigModel config);
}