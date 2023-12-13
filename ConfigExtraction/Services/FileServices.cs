namespace ConfigExtraction.Services;

using System;

/// <summary>
/// Various file services
/// </summary>
public class FileServices : IFileServices
{
  public string GetFullPath(string? fileName = null)
  {
    // Set the default file name
    if (string.IsNullOrEmpty(fileName))
    {
      fileName = SetDefaultFileName();
    }

    // Get the path of the file
    var basePath = Path.Combine(AppContext.BaseDirectory);
    var configFilePath = Path.Combine(basePath, fileName);

    return configFilePath;
  }

  /// <summary>
  /// Sets the default file name/path based on the solution mode.
  /// If developing locally, then debug will be enabled
  /// </summary>
  /// <returns>The defaulted file name</returns>
  private string SetDefaultFileName()
  {
    var defaultFileName = "config.json";
#if DEBUG
    // On our local the file is nested under the Files folder for tidiness
    return $"Files\\{defaultFileName}";
#else
    // When using the published exe the file will be inline with the exe
    return defaultFileName;
#endif
  }

  public bool Exists(string path)
  {
    var fileExists = File.Exists(path);

    return fileExists;
  }

  public string ReadText(string path)
  {
    var fileContents = File.ReadAllText(path);

    return fileContents ?? string.Empty;
  }
}

public interface IFileServices
{
  public string GetFullPath(string? fileName = null);
  public bool Exists(string path);
  public string ReadText(string path);
}