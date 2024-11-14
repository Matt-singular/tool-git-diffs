namespace Business.Domain.Commits;

using System.Threading.Tasks;
using Business.Models.Commits.GetRawCommits;
using Common.Shared.Config;
using Common.Shared.Models;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRawCommits"/>
public class GetRawCommits(IOptions<SecretSettings> secretSettings) : IGetRawCommits
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);

  /// <inheritdoc/>
  public Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request)
  {
    throw new NotImplementedException();
  }
}