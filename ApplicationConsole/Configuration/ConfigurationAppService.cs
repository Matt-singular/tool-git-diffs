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
      var propertyName = property.Name;
      var propertyValue = property.GetValue(secrets);

      switch (propertyValue)
      {
        case string str:
          HandleError(str, propertyName);
          break;
        case string[] strArray when strArray.Any():
          HandleError(strArray.First(), propertyName);
          break;
        default:
          throw new NotImplementedException($"Property type not handled for {propertyName}");
      }
    }
  }

  public void HandleError(string propertyValue, string propertyName)
  {
    if (string.IsNullOrWhiteSpace(propertyValue) || propertyValue.Equals("SECRET", StringComparison.CurrentCultureIgnoreCase))
    {
#if RELEASE
      var message = $"{propertyName} has not been set in the config.json";
      throw new ArgumentNullException(propertyName, message);
#endif
#if DEBUG
      var message = $"{propertyName} secret has not been configured, use the dotnet secrets-set comamnd to configure";
      throw new ArgumentNullException(propertyName, message);
#endif
    }
  }
}

public interface IConfigurationAppService
{
  public void Process();
}