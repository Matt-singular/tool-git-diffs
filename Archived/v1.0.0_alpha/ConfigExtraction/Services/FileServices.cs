namespace ConfigExtraction.Services;

using System;

/// <summary>
/// Various file services
/// </summary>
public class FileServices : IFileServices
{
  /// <summary>
  /// Sets the full file path using the provided file name
  /// </summary>
  /// <param name="fileName">The name of the file</param>
  /// <returns>The full file path in string format</returns>
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
  public virtual string SetDefaultFileName()
  {
#if DEBUG
    // On our local the file is nested under the Files folder for tidiness
    return Constants.defaultFileNameDebugMode;
#else
    // When using the published exe the file will be inline with the exe
    return Constants.defaultFileNameReleaseMode;
#endif
  }

  /// <summary>
  /// Checks if the file exists
  /// </summary>
  /// <param name="path">The path of the file to check</param>
  /// <returns>True if the file exists, Otherwise False</returns>
  public bool Exists(string path)
  {
    var fileExists = File.Exists(path);

    return fileExists;
  }

  /// <summary>
  /// Reads all of the content for the specified file and returns them in string format
  /// </summary>
  /// <param name="path">The path of the file to read from</param>
  /// <returns>The contents of the file</returns>
  public string ReadText(string path)
  {
    try
    {
      var fileContents = File.ReadAllText(path);

      return fileContents;
    }
    catch (Exception)
    {
      // Handle the errors with reading the file in the calling code
      return string.Empty;
    }
  }

  public static class Constants
  {
    private const string defaultConfigName = "config.json";
    public const string defaultFileNameDebugMode = $"Files//{defaultConfigName}";
    public const string defaultFileNameReleaseMode = defaultConfigName;
  }
}

public interface IFileServices
{
  public string GetFullPath(string? fileName = null);
  public string SetDefaultFileName();
  public bool Exists(string path);
  public string ReadText(string path);
}