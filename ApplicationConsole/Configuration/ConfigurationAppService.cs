namespace ApplicationConsole.Configuration;
using Microsoft.Extensions.Options;

public class ConfigurationAppService : IConfigurationAppService
{
  // Configuration - settings
  private readonly SecretsSettings SecretsSettings;

  public ConfigurationAppService(IOptions<SecretsSettings> secretSettings)
  {
    // Validation of secret configurations (if any aren't set then an error will occur here)
    this.SecretsSettings = secretSettings.Value;
  }

  public void Process()
  {
    ValidateSecrets(this.SecretsSettings);
  }

  public void ValidateSecrets(SecretsSettings secrets)
  {
    // Use reflection to iterate through the properties and check their values.
    var properties = typeof(SecretsSettings).GetProperties();
    var errorMessages = new List<(string propName, string msg)>();

    foreach (var property in properties)
    {
      var value = property.GetValue(secrets) as string;
      if (string.IsNullOrWhiteSpace(value) || value.Equals("SECRET", StringComparison.CurrentCultureIgnoreCase))
      {
#if RELEASE
        var message = $"{property.Name} has not been set in the config.json";
        throw new ArgumentNullException(property.Name, message);
#endif
#if DEBUG
      var message = $"{property.Name} secret has not been configured, use the dotnet secrets-set comamnd to configure";
      throw new ArgumentNullException(property.Name, message);
#endif
      }
    }
  }
}

public interface IConfigurationAppService
{
  public void Process();
}