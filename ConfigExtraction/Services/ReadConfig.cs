namespace ConfigExtraction.Services;

using System.Text.Json;
using Models;

public class ReadConfig : IReadConfig
{
  private readonly IFileServices fileServices;

  public ReadConfig(IFileServices fileServices)
  {
    this.fileServices = fileServices;
  }

  public ConfigModel Process()
  {
    try
    {
      // Get the path of the user's config.json file
      var configFilePath = this.fileServices.GetFullPath();

      // Check if the file exists, short-circuit if it doesn't
      var configJsonPresentRuleViolated = !this.fileServices.Exists(configFilePath);
      if (configJsonPresentRuleViolated)
      {
        // Short-circuit with a user friendly error message
        throw new FileNotFoundException(Constants.Errors.FileNotFound);
      }

      // Read the JSON content and deserialise it to an object
      var configJson = this.fileServices.ReadText(configFilePath) ?? string.Empty;
      var deserialisationOptions = GetJsonSerialiserOptions();
      var deserialisedObject = JsonSerializer.Deserialize<ConfigModel>(configJson, deserialisationOptions);

      // Check if the deserialisation was unsuccessful
      if (deserialisedObject is null || deserialisedObject.IsDefault())
      {
        // Short-circuit with a user friendly error message
        throw new JsonException(Constants.Errors.FailedDeserialisation);
      }

      return deserialisedObject;
    }
    catch (Exception ex)
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
      public const string FailedDeserialisation = "The config.json deserialisation was unsuccessful, please verify it's correctness and check the README for assistance";
    }
  }
}

public interface IReadConfig
{
  public ConfigModel Process();
}