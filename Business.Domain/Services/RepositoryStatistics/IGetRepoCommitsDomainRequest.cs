namespace Business.Domain.Services.RepositoryStatistics;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The domain request for the Raw and Cleaned commits logic
/// </summary>
public interface IGetRepoCommitsDomainRequest
{
  /// <summary>
  /// The repositories to pull data for
  /// </summary>
  public List<Repository> Repositories { get; set; }

  /// <summary>
  /// The from git reference for all of the repositories to use
  /// </summary>
  public string? FromBranchOrTag { get; set; }

  /// <summary>
  /// The to git reference for all of the repositories to use
  /// </summary>
  public string? ToBranchOrTag { get; set; }

  public class Repository
  {
    /// <summary>
    /// The name of the repository
    /// </summary>
    public required string RepositoryName { get; set; }

    /// <summary>
    /// The from git reference for the specified repository to use
    /// </summary>
    public string? FromBranchOrTag { get; set; }

    /// <summary>
    /// The to git reference for the specified repository to use
    /// </summary>
    public string? ToBranchOrTag { get; set; }
  }

  /// <summary>
  /// Validates the spcific request model instance
  /// </summary>
  /// <returns>True if valid, Otherwise False</returns>
  public bool ValidateModel();

  /// <summary>
  /// The implementation detail for the model validation
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  public static bool ValidateModel(IGetRepoCommitsDomainRequest model)
  {
    // Checks if global git references were supplied
    var hasGlobalFromBranchOrTag = !string.IsNullOrEmpty(model.FromBranchOrTag);
    var hasGlobalToBranchOrTag = !string.IsNullOrEmpty(model.ToBranchOrTag);

    // Checks each of the underlying repositories
    var isValidRequest = model.Repositories.All(repo =>
    {
      var isValidRepositoryName = !string.IsNullOrEmpty(repo.RepositoryName);
      var isValidFromBranchOrTag = hasGlobalFromBranchOrTag || !string.IsNullOrEmpty(repo.FromBranchOrTag);
      var isValidToBranchOrTag = hasGlobalToBranchOrTag || !string.IsNullOrEmpty(repo.ToBranchOrTag);
      return isValidRepositoryName && isValidFromBranchOrTag && isValidToBranchOrTag;
    });

    // The request is only valid if 
    return isValidRequest;
  }
}
