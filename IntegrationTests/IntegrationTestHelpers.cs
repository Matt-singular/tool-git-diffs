namespace IntegrationTests;

using Common.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

/// <summary>
/// Integration.Test Helpers
/// </summary>
public static class IntegrationTestHelpers
{
  public static IConfigurationRoot GetBuiltApplicationConfiguration()
  {
    // Gets the built application configuration (appsettings.json)
    var configuration = new ConfigurationBuilder()
      .SetupCommonSharedConfiguration()
      .Build();

    return configuration;
  }

  public static IOptions<TSettings> GetOptions<TSettings>(this IConfigurationRoot configuration, string sectionName) where TSettings : class
  {
    // Configures the settings class using the applicaton configuration
    var settings = (TSettings)Activator.CreateInstance(typeof(TSettings))!;
    configuration.GetSection(sectionName).Bind(settings!);

    return Options.Create(settings!);
  }
}