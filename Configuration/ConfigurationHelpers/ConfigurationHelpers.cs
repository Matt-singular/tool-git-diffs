namespace Configuration.ConfigurationHelpers;

/// <summary>
/// This class provides helper methods for configuration operations.
/// </summary>
public static class ConfigurationHelpers
{
  public static string GetProjectRootPath()
  {
    var appName = "tool-git-diffs";
    var appDomainPath = AppDomain.CurrentDomain.BaseDirectory;
    var appDomainRootPath = appDomainPath.Substring(0, appDomainPath.IndexOf(appName));

    return Path.Combine(appDomainRootPath, appName);
  }

  public static string GetConfigJsonPath()
  {
    // Gets the path for the config.json file programmatically
    var rootPath = GetProjectRootPath();
    var configJsonPath = Path.Combine(rootPath, "ApplicationConsole", "config.json");

    return configJsonPath;
  }

  public static string GetOutputFilePath(string outputPath)
  {
    var rootPath = GetProjectRootPath();
    var outputFilePath = Path.Combine(rootPath, outputPath);

    return outputFilePath;
  }
}