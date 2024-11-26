namespace Common.Shared.Config;

/// <summary>
/// Represents the secrets settings for the application as found in the appsettings.json file
/// </summary>
public class SecretSettings
{
  /// <summary>
  /// The personal access token to authenticate GitHub's Octokit API
  /// </summary>
  public string? GitHubAccessToken { get; set; }

  /// <summary>
  /// The GitHub organisation's name
  /// </summary>
  public string? GitHubOrganisationName { get; set; }

  /// <summary>
  /// The GitHub repository names
  /// </summary>
  public string[]? GitHubRepositories { get; set; }
}