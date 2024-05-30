namespace ApplicationConsole.Configuration;

/// <summary>
/// Represents the secrets settings for the application as found in config.json
/// </summary>
public class SecretsSettings
{
  public string? GitHubAccessToken { get; set; }
  public string? GitHubOrganisationName { get; set; }
  public string[] GitHubRepositories { get; set; }
}