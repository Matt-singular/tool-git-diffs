namespace Application.API.Controllers;

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
  public object GetRawExcelReport()
  {
    return new
    {
      Message = "get-repository-list"
    };
  }
}