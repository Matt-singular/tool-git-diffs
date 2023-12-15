namespace ConfigExtraction.Services;

using System.Text.Json;
using ConfigExtraction.Models;

/// <summary>
/// Validates the Config.json contents against relevant criteria
/// </summary>
public class ValidateConfig : IValidateConfig
{
  public ConfigModel? Config { get; set; } = null!;

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
    // Global-level Diff Range
    var config = this.Config ?? new();
    var globalLevel = config.DiffRange;

    // Check the global-level
    var (globalFromValue, globalToValue) = DiffRange.CheckDiffRangeSet(globalLevel);
    var globalLevelHasBothValues = globalFromValue && globalToValue;

    if (globalLevelHasBothValues)
    {
      // Short-circuit as both the from and the to values were set at the global-level
      return true;
    }

    // Repository-level Diff Range
    var repositoryLevelEnumerable = config.Repositories?.Select(repo => repo?.DiffRange);
    var repositoryLevel = repositoryLevelEnumerable?.ToList() ?? [];

    // Check the repository-level
    var repositoryLevelHasValidValues = repositoryLevel.All(repoLevel => CheckRepositoryLevelDiffRangeSelection(repoLevel, globalFromValue, globalToValue));
    var noDiffRangeSet = repositoryLevel.Count == 0;

    if (repositoryLevelHasValidValues == false || noDiffRangeSet)
    {
      // Short-circuit with an error as the repo-level diff ranges aren't sufficient
      throw new InvalidDataException(Constants.Errors.InvalidDiffRangeSelection);
    }

    // Valid diff range selection
    return true;
  }

  /// <summary>
  /// Checks the repository-level DiffRange values in-conjunction with the global DiffRange values
  /// </summary>
  /// <param name="repoLevel">The repository's DiffRange values</param>
  /// <param name="globalFromValue">The global-level DiffRange from value was set</param>
  /// <param name="globalToValue">The global-level DiffRange to value was set</param>
  /// <returns>True if there are valid repository-level DiffRange values, Otherwise False</returns>
  private bool CheckRepositoryLevelDiffRangeSelection(DiffRange? repoLevel, bool globalFromValue, bool globalToValue)
  {
    // Grab the repository-level set values
    var (repositoryFromValue, repositoryToValue) = DiffRange.CheckDiffRangeSet(repoLevel);

    // Check that either the repo-level has a value or the global-level has a value (both being set is also valid, so we don't check that)
    var validFromValue = repositoryFromValue || globalFromValue;
    var validToValue = repositoryToValue || globalToValue;

    // Valid scenario if we have both a from and to value set at the repo-level and/or the global-level
    return (validFromValue && validToValue);
  }

  /// <summary>
  /// Checks that all the commit references have patterns set
  /// </summary>
  /// <returns>True if the commit references have patterns</returns>
  /// <exception cref="InvalidDataException"></exception>
  public bool CheckCommitReferences()
  {
    // Get the commit reference parent patterns
    var config = this.Config ?? new();
    var referencePatternsEnumerable = config.References?.Select(reference => reference?.Pattern);
    var referencePatterns = referencePatternsEnumerable?.ToList() ?? [];

    // Check if ALL the reference patterns are valid (not checking sub items)
    var validReferencePatterns = referencePatterns.All(pattern => string.IsNullOrWhiteSpace(pattern) == false);
    var noReferencePatterns = referencePatterns.Count == 0;

    if (validReferencePatterns == false || noReferencePatterns)
    {
      // Short-circuit with an error as the commit reference patterns aren't set correctly
      throw new InvalidDataException(Constants.Errors.InvalidReferencePatterns);
    }

    // Valid commit references
    return true;
  }

  public static class Constants
  {
    public static class Errors
    {
      public const string IsDefaultDueToFailedDeserialisation = "The config.json deserialisation was unsuccessful, please verify its correctness and check the README for assistance";
      public const string InvalidDiffRangeSelection = "You have an invalid or incomplete diff range selection, please verify its correctness and check the README for assistance";
      public const string InvalidReferencePatterns = "You have an invalid or incomplete set of reference patterns, please verify its correctness and check the README for assistance";
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