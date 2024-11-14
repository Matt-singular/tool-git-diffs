namespace Business.Domain.Reports;

using System.Threading.Tasks;
using Business.Models.Commits.GetRawCommits;
using Business.Models.Reports.GetRawExcelReport;

/// <inheritdoc cref="IGetRawExcelReport"/>
public class GetRawExcelReport : IGetRawExcelReport
{
  /// <inheritdoc/>
  public Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request)
  {
    throw new NotImplementedException();
  }
}