﻿namespace ConfigExtraction.Services;

using System.Text.Json;
using ConfigExtraction.Models;

public class ValidateConfig : IValidateConfig
{
  public ConfigModel? Config { get; set; }

  public bool Process()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Validates whether the Config is default or not
  /// </summary>
  /// <returns>False if not default, Otherwise it throws an error</returns>
  /// <exception cref="JsonException"></exception>
  public bool CheckIfDefault()
  {
    var isDefault = this.Config?.IsDefault() ?? true;

    if (isDefault)
    {
      // The Config should only be default if the deserialisation has failed
      throw new JsonException(Constants.Errors.IsDefaultDueToFailedDeserialisation);
    }

    return isDefault;
  }

  /// <summary>
  /// Diff range values are required to be present in the Config.
  /// The Diff range values can appear at either (or both) the global level (Config.DiffRange)
  /// or the repository level (Config.Repositories.DiffRange).
  /// </summary>
  /// <returns>True if the Config conforms with the DiffRange requirements</returns>
  /// <exception cref="NotImplementedException"></exception>
  public bool CheckDiffRangeSelection()
  {
    throw new NotImplementedException();
  }

  public bool CheckCommitReferences()
  {
    throw new NotImplementedException();
  }

  public static class Constants
  {
    public static class Errors
    {
      public const string IsDefaultDueToFailedDeserialisation = "The config.json deserialisation was unsuccessful, please verify it's correctness and check the README for assistance";
    }
  }
}

public interface IValidateConfig
{
  public ConfigModel? Config { get; set; }
  public bool Process();
  public bool CheckIfDefault(); // Model not default (will need to modify ReadConfig)
  public bool CheckDiffRangeSelection();
  public bool CheckCommitReferences();
}