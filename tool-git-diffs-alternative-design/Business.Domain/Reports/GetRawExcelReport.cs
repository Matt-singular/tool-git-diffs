namespace Business.Domain.Reports;

using System.Threading.Tasks;
using Business.Models.Commits.GetRawCommits;
using Business.Models.Reports.GetRawExcelReport;
using Common.Shared.Config;
using Common.Shared.Models;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRawExcelReport"/>
public class GetRawExcelReport(IOptions<SecretSettings> secretSettings) : IGetRawExcelReport
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);

  /// <inheritdoc/>
  public Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request)
  {
    throw new NotImplementedException();
  }
}