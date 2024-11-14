namespace Business.Models.Repositories.GetRepositoryList;

/// <summary>
/// Fetches a list of repositories
/// </summary>
public interface IGetRepositoryList
{
    /// <summary>
    /// Processes the request to get a list of repositories
    /// </summary>
    /// <param name="request">Contains the data needed to get the list of repositories</param>
    /// <returns>A list of repositories</returns>
    public Task<GetRepositoryListResponse> ProcessAsync(GetRepositoryListRequest request);
}