namespace Business.Models.Reports.GetCleanedExcelReport;

/// <summary>
/// Contains the generated Excel report containing the cleaned commits from one or more repositories
/// </summary>
public class GetCleanedExcelReportResponse
{
  /// <summary>
  /// The excel sheet with commit data
  /// </summary>
  public byte[] ExcelFile { get; set; } = null!;
}