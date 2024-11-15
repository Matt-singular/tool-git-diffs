namespace Business.Models.Repositories.GetRepositoryDetail;

/// <summary>
/// Fetches details for a specific repository
/// </summary>
public interface IGetRepositoryDetail
{
  /// <summary>
  /// Processes the request to get the details of a repository
  /// </summary>
  /// <param name="request">Contains the data needed to do a detailed lookup of a repository</param>
  /// <returns>Repository-specific details</returns>
  public Task<GetRepositoryDetailResponse> ProcessAsync(GetRepositoryDetailRequest request);
}