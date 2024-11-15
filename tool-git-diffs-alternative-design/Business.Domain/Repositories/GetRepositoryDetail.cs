namespace Business.Domain.Repositories;

using System.Threading.Tasks;
using Business.Models.Repositories.GetRepositoryDetail;
using Common.Shared.Config;
using Common.Shared.Extensions.ThirdParty;
using Common.Shared.Models.Exceptions;
using Common.Shared.Models.Repositories;
using Common.Shared.Models.ThirdParty;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRepositoryDetail"/>
public class GetRepositoryDetail(IOptions<SecretSettings> secretSettings) : IGetRepositoryDetail
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);
  private string message = string.Empty;

  /// <inheritdoc/>
  public async Task<GetRepositoryDetailResponse> ProcessAsync(GetRepositoryDetailRequest request)
  {
    request.ValidateModel();

    var gitHubRepositorySummary = await this.TryGetRepositorySummaryFromGitHubAsync(request).ConfigureAwait(false);
    var gitHubRepositoryDetail = await this.TryGetRepositoryDetailFromGitHubAsync(gitHubRepositorySummary.Id).ConfigureAwait(false);

    return new()
    {
      Message = this.message,
      Summary = gitHubRepositorySummary.MapRepositorySummaryDetails(),
      Details = MapRepositoryDetail(gitHubRepositorySummary, gitHubRepositoryDetail)
    };
  }

  private async Task<Octokit.Repository> TryGetRepositorySummaryFromGitHubAsync(GetRepositoryDetailRequest request)
  {
    try
    {
      // Perform a lookup using either the repository id OR the repository name & owner
      var repositoryLookupTask = request.RepositoryId is not null
        ? this.apiClient.Repository.Get(request.RepositoryId.Value)
        : this.apiClient.Repository.Get(request.RepositoryOwner, request.RepositoryName);

      var repositoryLookupResponse = await repositoryLookupTask.ConfigureAwait(false);
      this.message = "Successfully retrieved the repository summary from GitHub";

      return repositoryLookupResponse;
    }
    catch (Exception)
    {
      this.message = "An error occurred while retrieving repository summary from GitHub";
      throw new GitHubApiException(message);
    }
  }

  private async Task<Octokit.CodeFrequency> TryGetRepositoryDetailFromGitHubAsync(long gitHubRepositoryId)
  {
    ArgumentNullException.ThrowIfNull(gitHubRepositoryId, nameof(gitHubRepositoryId));

    try
    {
      var getRepositoryStatisticsTask = this.apiClient.Repository.Statistics.GetCodeFrequency(gitHubRepositoryId);
      var getRepositoryStatisticsResponse = await getRepositoryStatisticsTask.ConfigureAwait(false);
      this.message = "Successfully retrieved the repository details from GitHub";

      return getRepositoryStatisticsResponse;
    }
    catch (Exception)
    {
      this.message = "An error occurred while retrieving the repository details from GitHub";
      throw new GitHubApiException(message);
    }
  }

  private static RepositoryDetail MapRepositoryDetail(Octokit.Repository gitHubRepositorySummary, Octokit.CodeFrequency gitHubRepositoryDetail)
  {
    return new()
    {
      CodeAdded = gitHubRepositoryDetail.MapRepositoryAdditions(),
      CodeDeleted = gitHubRepositoryDetail.MapRepositoryDeletions(),
      CreatedOn = gitHubRepositorySummary.CreatedAt,
      Age = gitHubRepositorySummary.CalculateRepositoryAge(),
      Visibility = gitHubRepositorySummary.MapRepositoryVisibility(),
    };
  }
}