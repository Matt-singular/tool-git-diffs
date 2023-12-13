namespace ConfigExtraction;

using Services;

/// <summary>
/// Orchestration of all config extraction related logic
/// </summary>
public class Orchestration : IOrchestration
{
  private readonly IReadConfig readConfig;

  public Orchestration(IReadConfig readConfig)
  {
    this.readConfig = readConfig;
  }

  public void Process()
  {
    // 1) Read in the config.json file
    var configContents = this.readConfig.Process();
  }
}

public interface IOrchestration
{
  public void Process();
}