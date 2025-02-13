namespace Application.API.Controllers;

using Business.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to pull the list of differences between two specific commits, branches or tags for specified repositories
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DiffsController : ControllerBase
{
  /// <summary>
  /// Get the list of differences between two specific commits, branches or tags 
  /// for the specified repository
  /// </summary>
  /// <param name="request">The specified repository and diff range</param>
  /// <returns>The diffs for the specified repository</returns>
  [HttpPost("get-diffs-for-repository")]
  public IActionResult GetDiffsForRepository([FromBody] DiffsRequest request)
  {
    ArgumentNullException.ThrowIfNull(request, nameof(request));
    return Ok();
  }

  /// <summary>
  /// Get the list of differences between specific two specific commits, branches or tags 
  /// for each repository specified
  /// </summary>
  /// <param name="requests">The specified repositories and diff ranges</param>
  /// <returns>The diffs for each each specified repository</returns>
  [HttpPost("get-diffs-for-repositories")]
  public IActionResult GetDiffsForRepositories([FromBody] List<DiffsRequest> requests)
  {
    ArgumentNullException.ThrowIfNull(requests, nameof(requests));
    return Ok();
  }
}