namespace Common.Shared.Services.Commits.GetRawCommits;

using Common.Shared.Models.Commits;

/// <summary>
/// A re-usable domain service responsible for retrieving raw commits for a single specified repository
/// </summary>
public interface IGetRawCommitsDomainService
{
  /// <summary>
  /// Processes the provided details to fetch raw (unprocessed) commits for a single repository
  /// </summary>
  /// <param name="repositoryId">The repository id as retrieved from the Octokit Api</param>
  /// <param name="fromReference">The git branch or tag to generate the diffs from</param>
  /// <param name="toReference">The git branch or tag to generate the diffs until</param>
  /// <returns>A list of raw (unprocessed) commits for a specified repository</returns>
  public Task<List<RawCommitDetails>> ProcessAsync(long repositoryId, string fromReference, string toReference);
}