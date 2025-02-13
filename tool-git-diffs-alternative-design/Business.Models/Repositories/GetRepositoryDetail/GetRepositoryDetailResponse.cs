namespace Business.Models.Repositories.GetRepositoryDetail;

using Common.Shared.Models.Repositories;

/// <summary>
/// Contains the repository-specific details
/// </summary>
public class GetRepositoryDetailResponse
{
  public required RepositoryDetail Details { get; set; }
  public required string Message { get; set; }
  public required RepositorySummary Summary { get; set; }
}