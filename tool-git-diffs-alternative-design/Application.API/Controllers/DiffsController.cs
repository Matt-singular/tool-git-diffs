namespace Application.API.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to pull diff information for specific repositories and differences
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DiffsController : Controller
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
      Message = "Hello World"
    };
  }
}