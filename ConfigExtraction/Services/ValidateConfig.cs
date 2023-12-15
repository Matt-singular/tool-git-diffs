namespace ConfigExtraction.Services;

using System.Text.Json;
using ConfigExtraction.Models;

public class ValidateConfig : IValidateConfig
{
  public ConfigModel Config { get; set; } = null!;

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
  /// <exception cref="InvalidDataException"></exception>
  public bool CheckDiffRangeSelection()
  {
    // Diff Range exists at the global and repository-level
    var globalLevel = this.Config.DiffRange;
    var repositoryLevel = this.Config.Repositories?.Select(repo => repo?.DiffRange)?.ToList() ?? [];

    // Check the global-level
    var globalLevelSetValues = DiffRange.CheckDiffRangeSet(globalLevel);
    var globalLevelHasBothValues = globalLevelSetValues.from && globalLevelSetValues.to;

    if (globalLevelHasBothValues)
    {
      // Short-circuit as both the from and the to values were set at the global-level
      return true;
    }

    // Check the repository-level
    var repositoryLevelHasValidValues = repositoryLevel.All(repoLevel =>
    {
      // Grab the repository-level set values
      var repositoryLevelSetValues = DiffRange.CheckDiffRangeSet(repoLevel);

      // Check that either the repo-level has a value or the global-level has a value (both being set is also valid, so we don't check that)
      var validFromValue = repositoryLevelSetValues.from || globalLevelSetValues.from;
      var validToValue = repositoryLevelSetValues.to || globalLevelSetValues.to;

      if (validFromValue && validToValue)
      {
        // Valid scenario as we have both a from and to value set at the repo-level and/or the global-level
        return true;
      }

      // Not a valid scenario
      return false;
    });

    var noDiffRangeSet = repositoryLevel.Count == 0;
    if (repositoryLevelHasValidValues == false || noDiffRangeSet)
    {
      // Short-circuit with an error as the repo-level diff ranges aren't sufficient
      throw new InvalidDataException(Constants.Errors.InvalidDiffRangeSelection);
    }

    // Valid diff range selection
    return true;
  }

  public bool CheckCommitReferences()
  {
    throw new NotImplementedException();
  }

  public static class Constants
  {
    public static class Errors
    {
      public const string IsDefaultDueToFailedDeserialisation = "The config.json deserialisation was unsuccessful, please verify its correctness and check the README for assistance";
      public const string InvalidDiffRangeSelection = "You have an invalid or incomplete diff range selection, please verify its correctness and check the README for assistance";
    }
  }
}

public interface IValidateConfig
{
  public ConfigModel Config { get; set; }
  public bool Process();
  public bool CheckIfDefault(); // Model not default (will need to modify ReadConfig)
  public bool CheckDiffRangeSelection();
  public bool CheckCommitReferences();
}