namespace Business.Domain.Commits;

using System.Threading.Tasks;
using Business.Models.Commits.GetCleanedCommits;
using Common.Shared.Config;
using Common.Shared.Models.ThirdParty;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetCleanedCommits"/>
public class GetCleanedCommits(IOptions<SecretSettings> secretSettings) : IGetCleanedCommits
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);

  /// <inheritdoc/>
  public Task<GetCleanedCommitsResponse> ProcessAsync(GetCleanedCommitsRequest request)
  {
    throw new NotImplementedException();
  }
}