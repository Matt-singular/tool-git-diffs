namespace Business.Models.Reports.GetCleanedExcelReport;

/// <summary>
/// Generates an excel report using cleaned commits
/// </summary>
public interface IGetCleanedExcelReport
{
  /// <summary>
  /// Processes the request to generate an excel report from the cleaned commits provided
  /// </summary>
  /// <param name="request">Contains the cleaned commit details broken down by repository</param>
  /// <returns>An excel report containing the cleaned commits data</returns>
  public Task<GetCleanedExcelReportResponse> ProcessAsync(GetCleanedExcelReportRequest request);
}