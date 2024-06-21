namespace TestHelpers;

using Configuration.ConfigurationHelpers;
using Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

/// <summary>
/// Provides helper methods for mocking configuration settings.
/// </summary>
public static class ConfigHelpers
{
  private static IConfigurationRoot? GetValuesFromConfigJson()
  {
    // Pull values from config.json
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(ConfigurationHelpers.GetConfigJsonPath())
        .AddUserSecrets<Program>()
        .Build();

    return config;
  }

  public static IOptions<SecretSettings> MockSecretSettings()
  {
    // Pull values from config.json
    var config = GetValuesFromConfigJson();

    // Mocked SecretSettings
    var secretSettings = config.GetSection("Secrets").Get<SecretSettings>();
    var mockedSecretSettings = Options.Create(secretSettings);

    return mockedSecretSettings;
  }

  public static IOptions<CommitSettings?> MockCommitSettings()
  {
    // Pull values from config.json
    var config = GetValuesFromConfigJson();

    // Mocked CommitSettings
    var commitSettings = config.GetSection("Commits").Get<CommitSettings>();
    var mockedCommitSettings = Options.Create(commitSettings);

    return mockedCommitSettings;
  }
}