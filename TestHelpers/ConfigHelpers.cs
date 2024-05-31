namespace TestHelpers;

using Configuration.ConfigurationHelpers;
using Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

public static class ConfigHelpers
{
  public static IOptions<SecretSettings> MockSecretSettings()
  {
    // Pull values from config.json
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(ConfigurationHelpers.GetConfigJsonPath())
        .AddUserSecrets<Program>()
        .Build();

    // Mocked SecretSettings
    var secretSettings = config.GetSection("Secrets").Get<SecretSettings>();
    var mockedSecretSettings = Options.Create(secretSettings);

    return mockedSecretSettings;
  }
}