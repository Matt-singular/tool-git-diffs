namespace ApplicationConsole;

/// <summary>
/// Orchestration of all the application console related logic
/// </summary>
public class Orchestration : IOrchestration
{
  private readonly ConfigExtraction.IOrchestration configExtractionOrchestration;

  public Orchestration(ConfigExtraction.IOrchestration configExtractionOrchestration)
  {
    this.configExtractionOrchestration = configExtractionOrchestration;
  }

  public void Process()
  {
    // 1) Config magics
    configExtractionOrchestration.Process();
    Console.WriteLine("Hello world");
  }
}

public interface IOrchestration
{
  public void Process();
}