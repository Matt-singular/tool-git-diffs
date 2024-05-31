namespace Configuration;

using System.Text.RegularExpressions;
using Configuration.Settings;
using Microsoft.Extensions.Options;

public class ValidateConfigurationAppService(IOptions<SecretSettings> secretSettings, IOptions<FileSettings> fileSettings, IOptions<CommitSettings> commitSettings) : IValidateConfigurationAppService
{
  // Configuration - settings
  private readonly SecretSettings SecretSettings = secretSettings.Value;
  private readonly FileSettings FileSettings = fileSettings.Value;
  private readonly CommitSettings CommitSettings = commitSettings.Value;

  public void Process()
  {
    ValidateSecretSettings(SecretSettings);
    ValidateFileSettings(FileSettings);
    ValidateCommitSettings(CommitSettings);
  }

  public void ValidateSecretSettings(SecretSettings secrets)
  {
    // Use reflection to iterate through the properties and check their values.
    var properties = typeof(SecretSettings).GetProperties();

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

  public void ValidateFileSettings(FileSettings settings)
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

    // Validate output path exists
    if (!Directory.Exists(getFilePath(settings.OutputPath)))
    {
      throw new DirectoryNotFoundException($"The directory {settings.OutputPath} does not exist.");
    }
  }

  public void ValidateCommitSettings(CommitSettings settings)
  {
    // Validate if there are any rules
    var hasRules = settings.Rules?.Any() ?? false;
    if (!hasRules)
    {
      // Short-circuit as there aren't any rules
      throw new ArgumentNullException("Commits.Rules", "No rules have been configured in the config.json");
    }

    // Validate Regex
    void isValidRegex(string pattern, string section, string header, bool isRequired = true)
    {
      if (isRequired && string.IsNullOrWhiteSpace(pattern))
      {
        // Short-circuit as there isn't a pattern
        throw new ArgumentNullException(header, $"Commits.{section} has not been set in the config.json");
      }

      try
      {
        new Regex(pattern);
      }
      catch (Exception ex)
      {
        // Short-circuit as the pattern is invalid
        throw new ArgumentException(header, $"Commits.{section} contaisn any invalid Regex pattern in config.json: {pattern}", ex);
      }
    }

    // Iterate through the Rules and validate the patterns
    foreach (var rule in settings.Rules)
    {
      // Validate groupBy
      isValidRegex(rule.GroupBy, nameof(rule.GroupBy), rule.Header, isRequired: true);

      // Validate the pattern (not required)
      isValidRegex(rule.Pattern, nameof(rule.Pattern), rule.Header, isRequired: false);
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

public interface IValidateConfigurationAppService
{
  public void Process();
}