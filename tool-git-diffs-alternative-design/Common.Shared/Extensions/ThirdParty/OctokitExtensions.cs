namespace Common.Shared.Extensions.ThirdParty;

using Common.Shared.Models.Repositories;

/// <summary>
/// Contains various extension methods for the Octokit third-party library
/// </summary>
public static class OctokitExtensions
{
  /// <summary>
  /// Maps summary-level repository details from a GitHub repository object
  /// </summary>
  /// <param name="gitHubRepository">The GitHub repository object</param>
  /// <returns>The summary-level repository details</returns>
  public static RepositorySummary MapRepositorySummaryDetails(this Octokit.Repository gitHubRepository)
  {
    ArgumentNullException.ThrowIfNull(gitHubRepository, nameof(gitHubRepository));

    return new()
    {
      RepositoryId = gitHubRepository.Id,
      RepositoryName = gitHubRepository.Name
    };
  }

  /// <summary>
  /// Calculates the repository's age from its creation date
  /// </summary>
  /// <param name="gitHubRepository">The GitHub repository object</param>
  /// <returns>The age fo the repository</returns>
  public static int CalculateRepositoryAge(this Octokit.Repository gitHubRepository)
  {
    var currentUtc = DateTime.UtcNow;
    var createdAtUtc = gitHubRepository.CreatedAt.UtcDateTime;

    var age = currentUtc.Year - createdAtUtc.Year;

    if (currentUtc < createdAtUtc.AddYears(age))
    {
      age--;
    }

    return age;
  }

  /// <summary>
  /// Maps the repository visibility from a GitHub repository object
  /// </summary>
  /// <param name="gitHubRepository">The GitHub repository object</param>
  /// <returns>The repository's visibility</returns>
  /// <exception cref="NotImplementedException"></exception>
  public static RepositoryVisibility MapRepositoryVisibility(this Octokit.Repository gitHubRepository)
  {
    return gitHubRepository.Visibility switch
    {
      Octokit.RepositoryVisibility.Public => RepositoryVisibility.Public,
      Octokit.RepositoryVisibility.Private => RepositoryVisibility.Private,
      Octokit.RepositoryVisibility.Internal or null or _ => throw new NotImplementedException()
    };
  }

  /// <summary>
  /// Maps the repository's code additions from a GitHub repository code frequency object
  /// </summary>
  /// <param name="gitHubRepositoryCodeFrequency">The GitHub repository code frequency detail</param>
  /// <returns>The code additions that have been made to the repository</returns>
  public static int MapRepositoryAdditions(this Octokit.CodeFrequency gitHubRepositoryCodeFrequency)
  {
    var additions = gitHubRepositoryCodeFrequency.AdditionsAndDeletionsByWeek.Sum(codeFrequency => codeFrequency.Additions);

    return additions;
  }

  /// <summary>
  /// Maps the repository's code deletions from a GitHub repository code frequency object
  /// </summary>
  /// <param name="gitHubRepositoryCodeFrequency">The GitHub repository code frequency detail</param>
  /// <returns>The code deletions that have been made to the repository</returns>
  public static int MapRepositoryDeletions(this Octokit.CodeFrequency gitHubRepositoryCodeFrequency)
  {
    var deletions = gitHubRepositoryCodeFrequency.AdditionsAndDeletionsByWeek.Sum(codeFrequency => codeFrequency.Deletions);

    return deletions;
  }

  /// <summary>
  /// Determines whether the current commit is a merge commit
  /// </summary>
  /// <param name="gitHubCommit">The GitHub commit details</param>
  /// <returns>True if the commit is a merge commit, Otherwise False</returns>
  public static bool CheckIsMergeCommit(this Octokit.GitHubCommit gitHubCommit)
  {
    var isMergeCommit = gitHubCommit.Parents.Count > 1;

    return isMergeCommit;
  }
}