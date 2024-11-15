namespace Business.Models.Repositories.GetRepositoryList;

using Common.Shared.Models.Repositories;

/// <summary>
/// Contains the list of repositories
/// </summary>
public class GetRepositoryListResponse
{
  public required string Message { get; set; }
  public required string Owner { get; set; }
  public required RepositoryOwnerTypes OwnerType { get; set; }
  public required List<RepositorySummary> Repositories { get; set; }
}