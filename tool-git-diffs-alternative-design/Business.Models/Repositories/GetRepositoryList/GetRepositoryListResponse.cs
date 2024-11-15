namespace Business.Models.Repositories.GetRepositoryList;

using Common.Shared.Models.Repositories;

/// <summary>
/// Contains the list of repositories
/// </summary>
public class GetRepositoryListResponse
{
  public string Message { get; set; } = null!;
  public string Owner { get; set; } = null!;
  public OwnerTypes OwnerType { get; set; }
  public List<RepositoryDetail> Repositories { get; set; } = [];
}