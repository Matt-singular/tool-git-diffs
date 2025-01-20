namespace Application.API.Controllers;

using Business.Models.Commits.GetCleanedCommits;
using Business.Models.Commits.GetRawCommits;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to pull diff information for specific repositories and differences
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CommitsController : Controller
{
  /// <summary>
  /// Gets the raw commits for the repository
  /// </summary>
  /// <returns>The raw unprocessed commits for the repository</returns>
  [HttpPost("get-raw-commits")]
  public async Task<GetRawCommitsResponse> GetRawCommitsAsync(
    [FromServices] IGetRawCommits getRawCommits,
    [FromBody] GetRawCommitsRequest request)
  {
    var getRawCommitsTask = getRawCommits.ProcessAsync(request);
    var getRawCommitsResponse = await getRawCommitsTask.ConfigureAwait(false);

    return getRawCommitsResponse;
  }

  /// <summary>
  /// Gets the cleaned commits for the repository
  /// </summary>
  /// <returns>The cleaned processed commits for the repository</returns>
  [HttpPost("get-cleaned-commits")]
  public async Task<GetCleanedCommitsResponse> GetCleanedCommitsAsync(
    // TODO: for now we are doing things like this*
    [FromServices] IGetRawCommits getRawCommits,
    [FromServices] IGetCleanedCommits getCleanedCommits,
    [FromBody] GetRawCommitsRequest request)
  {
    var getRawCommitsTask = getRawCommits.ProcessAsync(request);
    var getRawCommitsResponse = await getRawCommitsTask.ConfigureAwait(false);

    var getCleanedCommitsRequest = new GetCleanedCommitsRequest(getRawCommitsResponse.RepositoryRawCommits);
    var getCleanedCommitsTask = getCleanedCommits.ProcessAsync(getCleanedCommitsRequest);
    var getCleanedCommitsResponse = await getCleanedCommitsTask.ConfigureAwait(false);

    return getCleanedCommitsResponse;
  }
}