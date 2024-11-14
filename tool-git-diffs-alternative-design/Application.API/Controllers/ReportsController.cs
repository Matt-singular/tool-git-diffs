namespace Application.API.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to generate various reports
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportsController : Controller
{
  /// <summary>
  /// Gets the excel report containing raw commits
  /// </summary>
  /// <returns>The cleaned excel report</returns>
  [HttpGet("get-raw-excel-report")]
  public object GetRawExcelReport()
  {
    return new
    {
      Message = "get-raw-excel-report"
    };
  }

  /// <summary>
  /// Gets the excel report containing cleaned commits
  /// </summary>
  /// <returns>The raw excel report</returns>
  [HttpGet("get-cleaned-excel-report")]
  public object GetCleanedExcelReport()
  {
    return new
    {
      Message = "get-cleaned-excel-report"
    };
  }
}