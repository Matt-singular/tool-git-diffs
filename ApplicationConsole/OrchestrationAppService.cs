namespace ApplicationConsole;

using ApplicationConsole.Configuration;
using ApplicationConsole.Dependency;
using Microsoft.Extensions.Options;

public class OrchestrationAppService : IOrchestrationAppConsole
{
  // Configuration
  private readonly SecretSettings SecretsSettings;

  public OrchestrationAppService(IOptions<SecretSettings> secretSettings)
  {
    // Configuration
    this.SecretsSettings = secretSettings.Value;
  }

  public void Process()
  {
    var accessToken = this.SecretsSettings.GitHubAccessToken;
  }
}