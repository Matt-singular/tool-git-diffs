namespace Common.Shared.Models.Repositories;

/// <summary>
/// Contains details about a repository
/// </summary>
public class RepositoryDetail
{
  public long RepositoryId { get; set; }
  public string RepositoryName { get; set; } = null!;
}