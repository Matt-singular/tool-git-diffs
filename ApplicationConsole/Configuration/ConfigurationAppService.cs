namespace ApplicationConsole.Configuration;

using Microsoft.Extensions.Options;

public class ConfigurationAppService : IConfigurationAppService
{
  // Configuration - settings
  private readonly SecretSettings SecretSettings;
  private readonly FileSettings FileSettings;

  public ConfigurationAppService(IOptions<SecretSettings> secretSettings, IOptions<FileSettings> fileSettings)
  {
    // Configuration - settings
    this.SecretSettings = secretSettings.Value;
    this.FileSettings = fileSettings.Value;
  }

  public void Process()
  {
    ValidateSecrets(this.SecretSettings);
    ValidateFiles(this.FileSettings);
  }

  public void ValidateSecrets(SecretSettings secrets)
  {
    // Use reflection to iterate through the properties and check their values.
    var properties = typeof(SecretSettings).GetProperties();
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

  public void ValidateFiles(FileSettings settings)
  {
    static string getFilePath(string filePath)
    {
      if (!Directory.Exists(filePath))
      {
        // Get the full path
        var appName = "tool-git-diffs";
        var appDomainPath = AppDomain.CurrentDomain.BaseDirectory;
        var appDomainRootPath = appDomainPath.Substring(0, appDomainPath.IndexOf(appName));
        var fullFilePath = Path.Combine(appDomainRootPath, appName, filePath);
        return fullFilePath;
      }

      return filePath;
    }

    // validate filepath exists
    if (!Directory.Exists(getFilePath(settings.OutputPath)))
    {
      throw new DirectoryNotFoundException($"The directory {settings.OutputPath} does not exist.");
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