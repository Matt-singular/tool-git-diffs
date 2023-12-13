namespace ConfigExtraction.Services;

using System.Text.Json;
using Models;

public class ReadConfig
{
  public static ConfigModel Process()
  {
    // Get the path of the user's config.json file
    string basePath = Path.Combine(AppContext.BaseDirectory);
    var configFilePath = Path.Combine(basePath, "config.json");

    // Check if the file exists, short-circuit if it doesn't
    var configJsonPresentRuleViolated = !File.Exists(configFilePath);
    if (configJsonPresentRuleViolated)
    {
      // Short-circuit with a user friendly error message
      throw new FileNotFoundException("The config.json file is not present, please refer to the README for assistance");
    }

    // Read the JSON content and deserialise it to an object
    var configJson = File.ReadAllText(configFilePath) ?? string.Empty;
    var deserialisationOptions = GetJsonSerialiserOptions();
    var deserialisedObject = JsonSerializer.Deserialize<ConfigModel>(configJson, deserialisationOptions);

    // Check if the deserialisation was unsuccessful
    if (deserialisedObject is null)
    {
      // Short-circuit with a user friendly error message
      throw new JsonException("The config.json deserialisation was unsuccessful, please verify it's correctness and check the README for assistance");
    }

    return deserialisedObject;
  }

  private static JsonSerializerOptions GetJsonSerialiserOptions()
  {
    return new JsonSerializerOptions
    {
      // Ignores the property casing differences between the JSON and the DTO
      PropertyNameCaseInsensitive = true
    };
  }
}