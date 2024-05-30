namespace ApplicationConsole;

using ApplicationConsole.Configuration;
using ApplicationConsole.Dependency;
using Microsoft.Extensions.Options;

public class Orchestration : IOrchestrationAppConsole
{
  // Configuration
  private readonly SecretsSettings SecretsSettings;

  public Orchestration(IOptions<SecretsSettings> secretSettings)
  {
    // Configuration
    this.SecretsSettings = secretSettings.Value;
  }

  public void Process()
  {
    var accessToken = this.SecretsSettings.GitHubAccessToken;
  }
}