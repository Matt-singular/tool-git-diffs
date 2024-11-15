namespace Business.Domain.Repositories;

using System.Threading.Tasks;
using Business.Models.Repositories.GetRepositoryList;
using Common.Shared.Config;
using Common.Shared.Models.Exceptions;
using Common.Shared.Models.Repositories;
using Common.Shared.Models.ThirdParty;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRepositoryList"/>
public class GetRepositoryList(IOptions<SecretSettings> secretSettings) : IGetRepositoryList
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);
  private string owner = string.Empty;
  private OwnerTypes ownerType;
  private string message = string.Empty;

  /// <inheritdoc/>
  public async Task<GetRepositoryListResponse> ProcessAsync(GetRepositoryListRequest request)
  {
    request.ValidateModel();

    var gitHubRepositories = await this.GetRepositoryListFromGitHub(request).ConfigureAwait(false);

    var repositories = MapRepositoryList(gitHubRepositories);

    return new()
    {
      Message = this.message,
      Owner = this.owner,
      OwnerType = this.ownerType,
      Repositories = repositories
    };
  }

  private async Task<IReadOnlyList<Octokit.Repository>> GetRepositoryListFromGitHub(GetRepositoryListRequest request)
  {
    // Fetch the list of repositories from GitHub using the appropriate source
    return request switch
    {
      { OrganisationName: not null } => await TryGetRepositoryListForOrganisationFromGitHub(request.OrganisationName).ConfigureAwait(false),
      { UserName: not null } => await TryGetRepositoryListForUserFromGitHub(request.UserName).ConfigureAwait(false),
      _ => throw new InvalidOperationException()
    };
  }

  private async Task<IReadOnlyList<Octokit.Repository>> TryGetRepositoryListForOrganisationFromGitHub(string? organisationName)
  {
    ArgumentNullException.ThrowIfNull(nameof(organisationName), organisationName);

    this.owner = organisationName!;
    this.ownerType = OwnerTypes.Organisation;

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

  private async Task<IReadOnlyList<Octokit.Repository>> TryGetRepositoryListForUserFromGitHub(string? userName)
  {
    ArgumentNullException.ThrowIfNull(nameof(userName), userName);

    this.owner = userName!;
    this.ownerType = OwnerTypes.User;

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

  private static List<RepositoryDetail> MapRepositoryList(IReadOnlyList<Octokit.Repository> gitHubRepositories)
  {
    var mappedRepositories = gitHubRepositories
      .Select(organisationRepository => new RepositoryDetail
      {
        RepositoryId = organisationRepository.Id,
        RepositoryName = organisationRepository.Name
      })
      .OrderBy(repository => repository.RepositoryName)
      .ToList();

    return mappedRepositories;
  }
}