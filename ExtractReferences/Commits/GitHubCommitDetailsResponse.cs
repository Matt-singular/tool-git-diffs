namespace ExtractReferences.Commits;

public class GitHubCommitDetailsResponse
{
  /// <summary>
  /// The name of the repository
  /// </summary>
  public string? RepositoryName { get; set; }

  /// <summary>
  /// The name of the organisation that the repository belongs to (if applicable)
  /// </summary>
  public string? OrganisationName { get; set; }

  /// <summary>
  /// The repository's commit details
  /// </summary>
  public List<Commits>? CommitDetails { get; set; }

  public class Commits
  {
    /// <summary>
    /// The commit's hash used for unique identification
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// The name of the author that made the commit
    /// </summary>
    public string? AuthorName { get; set; }

    /// <summary>
    /// The email of the author that made the commit
    /// </summary>
    public string? AuthorEmail { get; set; }

    /// <summary>
    /// The commit message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The date the commit was made
    /// </summary>
    public DateTime? DateOfCommit { get; set; }

    /// <summary>
    /// Whether the commit is a simple merge or not 
    /// </summary>
    public bool IsMergeCommit { get; set; }
  }
}