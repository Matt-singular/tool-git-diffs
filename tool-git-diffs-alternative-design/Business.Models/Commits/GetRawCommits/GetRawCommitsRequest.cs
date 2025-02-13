namespace Business.Models.Commits.GetRawCommits;

/// <summary>
/// Contains the data needed to get the raw commits from one or more repositories
/// </summary>
public class GetRawCommitsRequest
{
  /// <summary>
  /// The lookup repositories
  /// </summary>
  public List<RepositoryLookupCriteria>? Repositories { get; set; }

  /// <summary>
  /// The Git reference (branch or tag) to pull from for all repositories unless overridden
  /// </summary>
  public string? FromReference { get; set; }

  /// <summary>
  /// The Git reference (branch or tag) to pull until for all repositories unless overridden
  /// </summary>
  public string? ToReference { get; set; }

  /// <inheritdoc cref="AddRepositoryToLookup(long?, string?, string?, string?, string?)"/>
  public void AddRepositoryToLookup(long repositoryId, string? fromReference = null, string? toReference = null)
    => this.AddRepositoryToLookup(repositoryId, null, null, fromReference, toReference);

  /// <inheritdoc cref="AddRepositoryToLookup(long?, string?, string?, string?, string?)"/>
  public void AddRepositoryToLookup(string repositoryName, string repositoryOwner, string? fromReference = null, string? toReference = null)
    => this.AddRepositoryToLookup(null, repositoryName, repositoryOwner, fromReference, toReference);

  /// <summary>
  /// Creates a <see cref="RepositoryLookupCriteria"/> instance and adds it to the <see cref="Repositories" /> collection
  /// </summary>
  /// <param name="repositoryId">The repository's id</param>
  /// <param name="repositoryName">The repository's name</param>
  /// <param name="repositoryOwner">The repository's owner</param>
  /// <param name="fromReference">The git reference override to pull from for this repository only</param>
  /// <param name="toReference">The git reference override to pull until for this repository only</param>
  private void AddRepositoryToLookup(long? repositoryId, string? repositoryName, string? repositoryOwner,
    string? fromReference = null, string? toReference = null)
  {
    // Use the class level FromReference and ToReference unless overrides are specified
    var repositoryLookup = new RepositoryLookupCriteria
    {
      RepositoryId = repositoryId,
      RepositoryName = repositoryName,
      RepositoryOwner = repositoryOwner,
      FromReference = fromReference ?? this.FromReference ?? throw new ArgumentNullException(this.FromReference, nameof(this.FromReference)),
      ToReference = toReference ?? this.ToReference ?? throw new ArgumentNullException(this.ToReference, nameof(this.ToReference)),
    };

    this.Repositories ??= [];
    this.Repositories.Add(repositoryLookup);
  }

  public class RepositoryLookupCriteria
  {
    /// <summary>
    /// The unique identifier for the repository (either it's id or name) in string format
    /// </summary>
    public string UniqueReference
    {
      get => this.RepositoryId is not null
        ? this.RepositoryId.ToString()!
        : this.RepositoryName
        ?? throw new ArgumentNullException("There is no unique repository reference");
    }

    /// <summary>
    /// The repository's GitHub Id
    /// </summary>
    public long? RepositoryId { get; set; }

    /// <summary>
    /// The repository's name
    /// </summary>
    public string? RepositoryName { get; set; }

    /// <summary>
    /// The repository's owner
    /// </summary>
    public string? RepositoryOwner { get; set; }

    /// <summary>
    /// The Git reference (branch or tag) to pull from for the specified repository
    /// </summary>
    public string FromReference { get; set; } = null!;

    /// <summary>
    /// The Git reference (branch or tag) to pull until for the specified repository
    /// </summary>
    public string ToReference { get; set; } = null!;
  }
}