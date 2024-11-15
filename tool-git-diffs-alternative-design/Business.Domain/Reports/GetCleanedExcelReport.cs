namespace Business.Domain.Reports;

using System.Threading.Tasks;
using Business.Models.Reports.GetCleanedExcelReport;
using Common.Shared.Config;
using Common.Shared.Models.ThirdParty;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetCleanedExcelReport"/>
public class GetCleanedExcelReport(IOptions<SecretSettings> secretSettings) : IGetCleanedExcelReport
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);

  /// <inheritdoc/>
  public Task<GetCleanedExcelReportResponse> ProcessAsync(GetCleanedExcelReportRequest request)
  {
    throw new NotImplementedException();
  }
}