namespace Common.Shared.Models.Repositories;

/// <summary>
/// Contains summary-level details about a repository
/// </summary>
public class RepositorySummary
{
  public long RepositoryId { get; set; }
  public string RepositoryName { get; set; } = null!;
}