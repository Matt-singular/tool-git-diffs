namespace Common.Shared.Models;

/// <summary>
/// Represnts a request to get the list of differences between two specific commits, branches or tags for a repository
/// </summary>
public interface IRepositoryDiffRequest
{
  /// <summary>
  /// The name of the repository
  /// </summary>
  public string RepositoryName { get; set; }

  /// <summary>
  /// The owner of the repository
  /// </summary>
  public string RepositoryOwner { get; set; }

  /// <summary>
  /// The reference to start the comparison from
  /// (e.g. commit hash, branch name, tag name)
  /// </summary>
  public string FromReference { get; set; }

  /// <summary>
  /// The reference to end the comparison at
  /// (e.g. commit hash, branch name, tag name)
  /// </summary>
  public string ToReference { get; set; }
}
