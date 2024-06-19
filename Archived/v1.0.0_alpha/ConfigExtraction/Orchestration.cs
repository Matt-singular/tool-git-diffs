namespace ConfigExtraction;

using ConfigExtraction.Models;
using Services;

/// <summary>
/// Orchestration of all config extraction related logic
/// </summary>
public class Orchestration(IReadConfig readConfig, IValidateConfig validateConfig) : IOrchestration
{
  public ConfigModel Process()
  {
    // 1) Read in the config.json file
    var configContents = readConfig.Process();

    // 2) Validate the config model
    validateConfig.Config = configContents;
    validateConfig.Process();

    // 3) Return the valid config's contents
    return configContents!;
  }
}

public interface IOrchestration
{
  public ConfigModel Process();
}