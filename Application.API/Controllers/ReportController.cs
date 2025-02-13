namespace Application.API.Controllers;

using Business.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to pull the list of differences between two specific commits, branches or tags for specified repositories
/// and then generate a report
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
  /// <summary>
  /// Get the list of differences between two specific commits, branches or tags 
  /// for the specified repository in Excel file format
  /// </summary>
  /// <param name="request">The specified repository and diff range to use for the report</param>
  /// <returns>The Excel report containing the diffs for the specified repository</returns>
  [HttpPost("get-excel-report-for-repository")]
  public IActionResult GetExcelDiffsReportForRepository([FromBody] ReportRequest request)
  {
    ArgumentNullException.ThrowIfNull(request, nameof(request));
    return Ok();
  }

  /// <summary>
  /// Get the list of differences between specific two specific commits, branches or tags 
  /// for each repository specified in Excel file format
  /// </summary>
  /// <param name="requests">The specified repositories and diff ranges to use for the report(s)</param>
  /// <returns>The Excel report(s) containing the diffs for each specified repository</returns>
  [HttpPost("get-excel-report-for-repositories")]
  public IActionResult GetExcelDiffsReportForRepositories([FromBody] List<ReportRequest> requests)
  {
    ArgumentNullException.ThrowIfNull(requests, nameof(requests));
    return Ok();
  }
}