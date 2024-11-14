namespace Application.API.Controllers;

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
  [HttpGet("get-raw-commits")]
  public object GetRawCommits()
  {
    return new
    {
      Message = "get-raw-commits"
    };
  }

  /// <summary>
  /// Gets the cleaned commits for the repository
  /// </summary>
  /// <returns>The cleaned processed commits for the repository</returns>
  [HttpGet("get-cleaned-commits")]
  public object GetCleanedCommits()
  {
    return new
    {
      Message = "get-cleaned-commits"
    };
  }
}