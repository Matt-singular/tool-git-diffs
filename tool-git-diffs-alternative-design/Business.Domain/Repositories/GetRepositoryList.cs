namespace Business.Domain.Repositories;

using System.Threading.Tasks;
using Business.Models.Repositories.GetRepositoryList;
using Common.Shared.Config;
using Common.Shared.Extensions.ThirdParty;
using Common.Shared.Models.Exceptions;
using Common.Shared.Models.Repositories;
using Common.Shared.Models.ThirdParty;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRepositoryList"/>
public class GetRepositoryList(IOptions<SecretSettings> secretSettings) : IGetRepositoryList
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);
  private string message = string.Empty;
  private string owner = string.Empty;
  private RepositoryOwnerTypes ownerType;

  /// <inheritdoc/>
  public async Task<GetRepositoryListResponse> ProcessAsync(GetRepositoryListRequest request)
  {
    request.ValidateModel();

    var gitHubRepositories = await this.GetRepositoryListFromGitHubAsync(request).ConfigureAwait(false);

    return new()
    {
      Message = this.message,
      Owner = this.owner,
      OwnerType = this.ownerType,
      Repositories = MapRepositoryList(gitHubRepositories)
    };
  }

  private async Task<IReadOnlyList<Octokit.Repository>> GetRepositoryListFromGitHubAsync(GetRepositoryListRequest request)
  {
    // Fetch the list of repositories from GitHub using the appropriate source
    return request switch
    {
      { OrganisationName: not null } => await TryGetRepositoryListForOrganisationFromGitHubAsync(request.OrganisationName).ConfigureAwait(false),
      { UserName: not null } => await TryGetRepositoryListForUserFromGitHubAsync(request.UserName).ConfigureAwait(false),
      _ => throw new InvalidOperationException()
    };
  }

  private async Task<IReadOnlyList<Octokit.Repository>> TryGetRepositoryListForOrganisationFromGitHubAsync(string? organisationName)
  {
    ArgumentNullException.ThrowIfNull(organisationName, nameof(organisationName));

    this.owner = organisationName!;
    this.ownerType = RepositoryOwnerTypes.Organisation;

    try
    {
      var organisationRepositoriesTask = this.apiClient.Repository.GetAllForOrg(organisationName);
      var organisationRepositoriesResponse = await organisationRepositoriesTask.ConfigureAwait(false);
      this.message = "Successfully retrieved repositories for the specified organisation";

      return organisationRepositoriesResponse;
    }
    catch (Exception)
    {
      this.message = "An error occurred while retrieving repositories for the specified organisation";
      throw new GitHubApiException(message);
    }
  }

  private async Task<IReadOnlyList<Octokit.Repository>> TryGetRepositoryListForUserFromGitHubAsync(string? userName)
  {
    ArgumentNullException.ThrowIfNull(userName, nameof(userName));

    this.owner = userName!;
    this.ownerType = RepositoryOwnerTypes.User;

    try
    {
      var userRepositoriesTask = this.apiClient.Repository.GetAllForUser(userName);
      var userRepositoriesResponse = await userRepositoriesTask.ConfigureAwait(false);
      this.message = "Successfully retrieved repositories for the specified user";

      return userRepositoriesResponse;
    }
    catch (Exception)
    {
      this.message = "An error occurred while retrieving repositories for the specified user";
      throw new GitHubApiException(message);
    }
  }

  private static List<RepositorySummary> MapRepositoryList(IReadOnlyList<Octokit.Repository> gitHubRepositories)
  {
    var mappedRepositories = gitHubRepositories
      .Select(gitHubRepository => gitHubRepository.MapRepositorySummaryDetails())
      .OrderBy(repository => repository.RepositoryName)
      .ToList();

    return mappedRepositories;
  }
}