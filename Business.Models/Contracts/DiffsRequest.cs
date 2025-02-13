namespace Business.Models.Contracts;

/// <summary>
/// The request object to get the list of differences between two specific commits, branches or tags
/// for specified repositories
/// </summary>
public record DiffsRequest
{
  /// <summary>
  /// The name of the repository
  /// </summary>
  public required string RepositoryName { get; set; }

  /// <summary>
  /// The owner of the repository
  /// </summary>
  public required string RepositoryOwner { get; set; }

  /// <summary>
  /// The reference to start the comparison from
  /// (e.g. commit hash, branch name, tag name)
  /// </summary>
  public required string FromReference { get; set; }

  /// <summary>
  /// The reference to end the comparison at
  /// (e.g. commit hash, branch name, tag name)
  /// </summary>
  public required string ToReference { get; set; }
}