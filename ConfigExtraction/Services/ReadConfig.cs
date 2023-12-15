namespace ConfigExtraction.Services;

using System.Text.Json;
using Models;

public class ReadConfig(IFileServices fileServices) : IReadConfig
{
  /// <summary>
  /// Read in the config.json file
  /// </summary>
  /// <returns>The contents of the config.json file</returns>
  /// <exception cref="FileNotFoundException"></exception>
  public ConfigModel? Process()
  {
    try
    {
      // Get the path of the user's config.json file
      var configFilePath = fileServices.GetFullPath();

      // Check if the file exists, short-circuit if it doesn't
      var configJsonPresentRuleViolated = !fileServices.Exists(configFilePath);
      if (configJsonPresentRuleViolated)
      {
        // Short-circuit with a user friendly error message
        throw new FileNotFoundException(Constants.Errors.FileNotFound);
      }

      // Read the JSON content and deserialise it to an object
      var configJson = fileServices.ReadText(configFilePath);
      var deserialisationOptions = GetJsonSerialiserOptions();
      var deserialisedObject = JsonSerializer.Deserialize<ConfigModel>(configJson, deserialisationOptions);

      return deserialisedObject;
    }
    catch (Exception)
    {
      // To ensure other JSON exceptions are caught
      throw;
    }
  }

  private static JsonSerializerOptions GetJsonSerialiserOptions()
  {
    return new JsonSerializerOptions
    {
      // Ignores the property casing differences between the JSON and the DTO
      PropertyNameCaseInsensitive = true
    };
  }

  public static class Constants
  {
    public static class Errors
    {
      public const string FileNotFound = "The config.json file is not present, please refer to the README for assistance";
    }
  }
}

public interface IReadConfig
{
  public ConfigModel? Process();
}