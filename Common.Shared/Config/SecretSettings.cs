namespace Common.Shared.Config;

/// <summary>
/// Represents the secrets settings for the application as found in the appsettings.json file
/// </summary>
public class SecretSettings
{
  public string? GitHubAccessToken { get; set; }
  public string? GitHubOrganisationName { get; set; }
  public string[]? GitHubRepositories { get; set; }
}