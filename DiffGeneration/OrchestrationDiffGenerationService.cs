namespace DiffGeneration;

using Configuration.Settings;
using Microsoft.Extensions.Options;

public class OrchestrationDiffGenerationService(IOptions<CommitSettings> commitSettings) : IOrchestrationDiffGenerationService
{
  private readonly CommitSettings CommitSettings = commitSettings.Value;

  public Task ProcessAsync(string build, string from, string to)
  {
    throw new NotImplementedException();
  }

  public Task PullRawDiffs()
  {
    throw new NotImplementedException();
  }

  public Task CleanDiffs()
  {
    throw new NotImplementedException();
  }

  public Task GenerateTextFile()
  {
    throw new NotImplementedException();
  }
}

public interface IOrchestrationDiffGenerationService
{
  Task ProcessAsync(string build, string from, string to);
  Task PullRawDiffs();
  Task CleanDiffs();
  Task GenerateTextFile();
}