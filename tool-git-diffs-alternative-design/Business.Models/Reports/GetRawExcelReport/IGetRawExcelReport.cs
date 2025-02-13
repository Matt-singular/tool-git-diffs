namespace Business.Models.Reports.GetRawExcelReport;

using Business.Models.Commits.GetRawCommits;

/// <summary>
/// Generates an excel report using raw commits
/// </summary>
public interface IGetRawExcelReport
{
  /// <summary>
  /// Processes the request to generate an excel report from the raw commits provided
  /// </summary>
  /// <param name="request">Contains the raw commit details broken down by repository</param>
  /// <returns>An excel report containing the raw commits data</returns>
  public Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request);
}