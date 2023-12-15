namespace ApplicationConsole;

/// <summary>
/// Orchestration of all the application console related logic
/// </summary>
public class Orchestration(ConfigExtraction.IOrchestration configExtraction) : IOrchestration
{
  public void Process()
  {
    // 1) Config read and validation
    configExtraction.Process();

    // Hello :)
    Console.WriteLine("Hello world");
  }
}

public interface IOrchestration
{
  public void Process();
}