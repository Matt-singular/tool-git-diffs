namespace ConfigExtraction;

using Services;

/// <summary>
/// Orchestration of all config extraction related logic
/// </summary>
public class ConfigExtraction
{
  public static void Process() // TODO: placeholder for now
  {
    // 1) Read in the config.json file
    var configContents = ReadConfig.Process();
  }
}