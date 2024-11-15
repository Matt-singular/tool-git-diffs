namespace Application.API.Controllers;

using Business.Models.Repositories.GetRepositoryList;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to fetch various repository-specific detail
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RepositoriesController : Controller
{
  /// <summary>
  /// Gets a list of repositories based on provided input
  /// </summary>
  /// <param name="getRepositoryList">The getRepositoryList domain service</param>
  /// <param name="request">The provided organisation name (optional if the user name was provided)</param>
  /// <returns>The list of repositories</returns>
  [HttpGet("get-repository-list")]
  public async Task<GetRepositoryListResponse> GetRepositoryListAsync(
    [FromServices] IGetRepositoryList getRepositoryList,
    [FromQuery] GetRepositoryListRequest request)
  {
    var getRepositoryListTask = getRepositoryList.ProcessAsync(request);
    var getRepositoryListResponse = await getRepositoryListTask.ConfigureAwait(false);

    return getRepositoryListResponse;
  }
}