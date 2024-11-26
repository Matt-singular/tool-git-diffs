namespace Common.Shared.Services.Commits.GetCleanedCommits;

using System.Collections.Generic;
using Common.Shared.Models.Commits;

/// <summary>
/// A re-usable domain service responsible for processing & cleaning the provided raw commits
/// </summary>
public interface IGetCleanedCommitsDomainService
{
  /// <summary>
  /// Processes (cleans) the provided raw commits
  /// </summary>
  /// <param name="rawCommits">The raw (unprocessed) GitHub commit details</param>
  /// <returns>A list of cleaned (processed) commits</returns>
  public List<CleanedCommitDetails> Process(List<RawCommitDetails> rawCommits);
}