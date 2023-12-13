namespace ConfigExtraction.Services;

using System;

/// <summary>
/// Various file services
/// </summary>
public class FileServices : IFileServices
{
  public string GetFullPath(string fileName = "config.json")
  {
    var basePath = Path.Combine(AppContext.BaseDirectory);
    var configFilePath = Path.Combine(basePath, fileName);

    return configFilePath;
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
  public string GetFullPath(string fileName = "config.json");
  public bool Exists(string path);
  public string ReadText(string path);
}