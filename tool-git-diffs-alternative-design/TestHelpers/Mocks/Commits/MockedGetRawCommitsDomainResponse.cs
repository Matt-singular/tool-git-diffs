namespace TestHelpers.Mocks.Commits;

using Common.Shared.Models.Commits;

/// <summary>
/// A mocked collection of <see cref="RawCommitDetails"/>
/// </summary>
public static class MockedGetRawCommitsDomainResponse
{
  /// <summary>
  /// Creates a mocked collection of raw commit details with commit messages linked to specific commit setting patterns
  /// </summary>
  /// <returns>The mocked data</returns>
  public static List<RawCommitDetails> CreateGetRawCommitsDomainResponseScenarioA()
  {
    return
    [
      new(){ Message = "FEAT-1000 added a thing" },
      new(){ Message = "No JIRA references" },
      new(){ Message = "FEAT200 No hyphen test" },
      new(){ Message = "feat3000 ignore case check" },
      new(){ Message = "DEFECT-1250 another reference" },
      new(){ Message = "FEAT-475, DEV2155, dev-125 Multiple references test" },
      new(){ Message = "Merge Pull request for FEAT-215", IsMergeCommit = true }
    ];
  }
}