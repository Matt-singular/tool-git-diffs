namespace Business.Domain.Repositories;

using System.Threading.Tasks;
using Business.Models.Repositories.GetRepositoryList;
using Common.Shared.Config;
using Common.Shared.Models;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRepositoryList"/>
public class GetRepositoryList(IOptions<SecretSettings> secretSettings) : IGetRepositoryList
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);

  /// <inheritdoc/>
  public Task<GetRepositoryListResponse> ProcessAsync(GetRepositoryListRequest request)
  {
    throw new NotImplementedException();
  }
}