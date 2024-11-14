namespace Business.Models.Commits.GetRawCommits;

/// <summary>
/// Fetches the raw commits from one or more repositories
/// </summary>
public interface IGetRawCommits
{
  /// <summary>
  /// Processes the request to fetch the raw commits from one or more repositories
  /// </summary>
  /// <param name="request">Contains details of each repository to fetch from</param>
  /// <returns>The raw commits response broken down by repository</returns>
  public Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request);
}