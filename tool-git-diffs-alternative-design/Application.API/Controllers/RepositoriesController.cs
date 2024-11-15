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
  /// <returns>The list of repositories</returns>
  [HttpGet("get-repository-list")]
  public async Task<object> GetRepositoryListAsync(
    [FromServices] IGetRepositoryList getRepositoryList,
    [FromQuery] string? organisationName,
    [FromQuery] string? userName)
  {
    var request = new GetRepositoryListRequest
    {
      OrganisationName = organisationName,
      UserName = userName
    };

    var getRepositoryListTask = getRepositoryList.ProcessAsync(request);
    var getRepositoryListResponse = await getRepositoryListTask.ConfigureAwait(false);

    return getRepositoryListResponse;
  }
}