namespace Business.Models.Commits.GetCleanedCommits;

using Common.Shared.Models.Commits;
using static Business.Models.Commits.GetCleanedCommits.GetCleanedCommitsResponse;

/// <summary>
/// Contains the cleaned commits response broken down by repository
/// </summary>
public class GetCleanedCommitsResponse(List<RepositoryCleanedCommitsDetail> repositoryCleanedCommits)
{
  /// <summary>
  /// All of the cleaned commits across all repositories
  /// </summary>
  public List<CleanedCommitDetails> RawCommits
  {
    get => this.RepositoryCleanedCommits
      .SelectMany(repository => repository.CleanedCommits)
      .ToList();
  }

  /// <summary>
  /// The cleaned commits broken down by repository
  /// </summary>
  public List<RepositoryCleanedCommitsDetail> RepositoryCleanedCommits { get; set; } = repositoryCleanedCommits;

  public class RepositoryCleanedCommitsDetail
  {
    /// <summary>
    /// The repository's name (or id) that the cleaned commits are associated with
    /// </summary>
    public string RepositoryName { get; set; } = null!;

    /// <summary>
    /// The cleaned commits associated with the repository
    /// </summary>
    public List<CleanedCommitDetails> CleanedCommits { get; set; } = null!;

    /// <summary>
    /// The git reference that the diff was pulled from
    /// </summary>
    public string FromReference { get; set; } = null!;

    /// <summary>
    /// The git reference that the diff was pulled until
    /// </summary>
    public string ToReference { get; set; } = null!;
  }
}