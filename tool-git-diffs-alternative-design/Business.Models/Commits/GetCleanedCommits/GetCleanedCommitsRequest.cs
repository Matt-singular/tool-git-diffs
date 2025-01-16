namespace Business.Models.Commits.GetCleanedCommits;

using Common.Shared.Models.Commits;
using static Business.Models.Commits.GetRawCommits.GetRawCommitsResponse;

/// <summary>
/// Contains a list of raw commits broken down by repository to process and clean
/// </summary>
public class GetCleanedCommitsRequest(List<RepositoryRawCommitsDetail> repositoryRawCommits)
{
  /// <summary>
  /// Raw commits to process and clean broken down by repository
  /// </summary>
  public List<RepositoryRawCommitsDetail> RepositoryRawCommits { get; set; } = repositoryRawCommits;
}