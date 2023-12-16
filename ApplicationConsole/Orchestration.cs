namespace ApplicationConsole;

/// <summary>
/// Orchestration of all the application console related logic
/// </summary>
public class Orchestration(ConfigExtraction.IOrchestration configExtraction, ReferenceExtraction.IOrchestration referenceExtraction) : IOrchestration
{
  public void Process()
  {
    // 1) Config read and validation
    var configContents = configExtraction.Process();

    // 2) Phase Two - Generate diffs and validation (for now just taking in the already present diff)
    var mockedCommits = Constants.MockedCommitFileContents.Split("\n").ToList(); // TODO: Phase One

    // 3) Extract the commit ticket references
    referenceExtraction.Process(mockedCommits, configContents);

    // Hello :)
    Console.WriteLine("Hello world");
  }

  public static class Constants
  {
    // TODO:
    // Focused on Phase one currrently, so just hardcoded here for now
    // (1) First update will be to pull commit messages from a provided text file
    // (2) Second update will be in Phase two where we will the git diff commands to pull the diffs in the application
    // (3) Third update will be after Phase two where we will probably add a layer - validate repo & fetch latest)
    public static readonly string MockedCommitFileContents =
      """
      FEATURE-1252 a commit with a reference
      FEATURE-1245 FEATURE-1251 a commit with two references
      FEATURE-235, FEATURE125 two references one with a typo
      No feature references here
      feature-3463 task-3256 task-3265 a feature with some sub items that should be grouped
      FEATURE-1252 a feature reference we've already seen (discard duplicates)
      """;
  }
}

public interface IOrchestration
{
  public void Process();
}