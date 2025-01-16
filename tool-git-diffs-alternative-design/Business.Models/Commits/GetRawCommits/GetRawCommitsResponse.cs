namespace Business.Models.Commits.GetRawCommits;

using System.Collections.Generic;
using Common.Shared.Models.Commits;
using static Business.Models.Commits.GetRawCommits.GetRawCommitsResponse;

/// <summary>
/// Contains the raw commits response broken down by repository
/// </summary>
public class GetRawCommitsResponse(List<RepositoryRawCommitsDetail> repositoryRawCommits)
{
  /// <summary>
  /// All of the raw commits across all repositories
  /// </summary>
  public List<RawCommitDetails> RawCommits
  {
    get => this.RepositoryRawCommits
      .SelectMany(repository => repository.RawCommits)
      .ToList();
  }

  /// <summary>
  /// The raw commits broken down by repository
  /// </summary>
  public List<RepositoryRawCommitsDetail> RepositoryRawCommits { get; set; } = repositoryRawCommits;

  public class RepositoryRawCommitsDetail
  {
    /// <summary>
    /// The repository's name (or id) that the raw commits are associated with
    /// </summary>
    public string RepositoryName { get; set; } = null!;

    /// <summary>
    /// The raw commits associated with the repository
    /// </summary>
    public List<RawCommitDetails> RawCommits { get; set; } = null!;

    /// <summary>
    /// The git reference that the raw commits were pulled from
    /// </summary>
    public string FromReference { get; set; } = null!;

    /// <summary>
    /// The git reference that the raw commits were pulled until
    /// </summary>
    public string ToReference { get; set; } = null!;
  }
}