namespace Business.Models.Commits.GetCleanedCommits;

/// <summary>
/// Takes in a set of raw commits from one or more repositories and processes (cleans) them
/// </summary>
public interface IGetCleanedCommits
{
  /// <summary>
  /// Processes the request to clean up the provided raw commits broken down by repository
  /// </summary>
  /// <param name="request">Contains details of each repository to fetch from</param>
  /// <returns>The cleaned commits response broken down by repository</returns>
  public Task<GetCleanedCommitsResponse> ProcessAsync(GetCleanedCommitsRequest request);
}