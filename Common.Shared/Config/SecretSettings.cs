namespace Common.Shared.Config;

/// <summary>
/// Represents the secrets settings for the application as found in config.json
/// </summary>
public class SecretSettings
{
  public required string GitHubAccessToken { get; set; }
  public required string GitHubOrganisationName { get; set; }
  public required string[] GitHubRepositories { get; set; }
}