namespace Common.Shared.Extensions.Config;

using System;
using System.Text.RegularExpressions;
using Common.Shared.Config;

/// <summary>
/// Config/settings related extension methods
/// </summary>
public static class ConfigExtensions
{
  /// <summary>
  /// Instantiates a <see cref="Regex"/> instance from the configured regex pattern
  /// </summary>
  /// <param name="commitSettingPattern">The regex pattern as found in the appsettings.json</param>
  /// <returns>The instantiated regex instance</returns>
  public static List<Regex> CreateRegexes(this CommitSettings commitSettings)
  {
    ArgumentNullException.ThrowIfNull(commitSettings.Patterns, nameof(commitSettings.Patterns));

    var regexes = commitSettings.Patterns
      .OrderBy(pattern => pattern.Priority)
      .Select(CreateRegex)
      .ToList();

    return regexes;
  }

  private static Regex CreateRegex(this CommitSettings.Pattern commitSettingPattern)
  {
    var regexPattern = commitSettingPattern.Regex;
    ArgumentNullException.ThrowIfNull(regexPattern, nameof(commitSettingPattern.Regex));

    var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

    return regex;
  }
}