namespace Business.Domain.Reports;

using System.Threading.Tasks;
using Business.Models.Reports.GetCleanedExcelReport;

/// <inheritdoc cref="IGetCleanedExcelReport"/>
public class GetCleanedExcelReport : IGetCleanedExcelReport
{
  /// <inheritdoc/>
  public Task<GetCleanedExcelReportResponse> ProcessAsync(GetCleanedExcelReportRequest request)
  {
    throw new NotImplementedException();
  }
}