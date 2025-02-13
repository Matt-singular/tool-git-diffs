namespace Application.API.Controllers;

using Business.Models.Commits.GetCleanedCommits;
using Business.Models.Commits.GetRawCommits;
using Business.Models.Reports.GetCleanedExcelReport;
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
  [HttpPost("get-cleaned-excel-report")]

  public async Task<object> GetCleanedExcelReport(
    // TODO: for now we are doing things like this*
    [FromServices] IGetRawCommits getRawCommits,
    [FromServices] IGetCleanedCommits getCleanedCommits,
    [FromServices] IGetCleanedExcelReport getCleanedExcelReport,
    [FromBody] GetRawCommitsRequest request)
  {
    var getRawCommitsTask = getRawCommits.ProcessAsync(request);
    var getRawCommitsResponse = await getRawCommitsTask.ConfigureAwait(false);

    var getCleanedCommitsRequest = new GetCleanedCommitsRequest(getRawCommitsResponse.RepositoryRawCommits);
    var getCleanedCommitsTask = getCleanedCommits.ProcessAsync(getCleanedCommitsRequest);
    var getCleanedCommitsResponse = await getCleanedCommitsTask.ConfigureAwait(false);

    var getCleanedExcelReportRequest = new GetCleanedExcelReportRequest(getCleanedCommitsResponse.RepositoryCleanedCommits);
    var getCleanedExcelReportResponse = getCleanedExcelReport.Process(getCleanedExcelReportRequest);

    return getCleanedExcelReportResponse;
  }
}