namespace TestHelpers.Mocks.Commits;

using Common.Shared.Models.Commits;

/// <summary>
/// A mocked collection of <see cref="CleanedCommitDetails"/>
/// </summary>
public static class MockedGetCleanedCommitsDomainResponse
{
  /// <summary>
  /// Creates a mocked collection of cleaned commit details with commit messages linked to specific commit setting patterns
  /// </summary>
  /// <returns>The mocked data</returns>
  public static List<CleanedCommitDetails> CreateGetCleanedCommitsDomainResponseScenarioA()
  {
    return
    [
      new CleanedCommitDetails(new(){ Message = "FEAT-1000 added a thing" })
      {
        JiraReferences =
        [
          new() { JiraReference = "FEAT-1000", Priority = 0 },
        ]
      },
      new CleanedCommitDetails(new(){ Message = "No JIRA references" })
      {
        JiraReferences = []
      },
      new CleanedCommitDetails(new(){ Message = "FEAT200 No hyphen test" })
      {
        JiraReferences =
        [
          new() { JiraReference = "FEAT200", Priority = 0 }
        ]
      },
      new CleanedCommitDetails(new(){ Message = "feat3000 ignore case check" })
      {
        JiraReferences =
        [
          new() { JiraReference = "feat3000", Priority = 0 }
        ]
      },
      new CleanedCommitDetails(new(){ Message = "DEFECT-1250 another reference" })
      {
        JiraReferences =
        [
          new() { JiraReference = "DEFECT-1250", Priority = 1 }
        ]
      },
      new CleanedCommitDetails(new(){ Message = "FEAT-475, DEV2155, dev-125 Multiple references test" })
      {
        JiraReferences =
        [
          new() { JiraReference = "FEAT-475", Priority = 0 },
          new() { JiraReference = "DEV2155", Priority = 2 },
          new() { JiraReference = "dev-125", Priority = 2 },
        ]
      },
      new CleanedCommitDetails(new(){ Message = "Merge Pull request for FEAT-215", IsMergeCommit = true })
      {
        JiraReferences =
        [
          new() { JiraReference = "FEAT-215", Priority = 0 }
        ]
      }
    ];
  }
}