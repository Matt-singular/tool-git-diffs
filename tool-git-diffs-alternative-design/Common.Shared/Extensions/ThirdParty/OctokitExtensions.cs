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
}