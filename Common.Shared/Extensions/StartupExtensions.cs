namespace Common.Shared.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public static class StartupExtensions
{
  /// <summary>
  /// Sets up the application configuration.
  /// </summary>
  /// <returns>The host builder with the configured application settings.</returns>
  public static IHostBuilder SetupCommonSharedConfiguration<TStartupClass>(this IHostBuilder builder)
    where TStartupClass : class
  {
    // Add config.json file
    builder.ConfigureAppConfiguration((context, config) => config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true));

    // Enable User Secrets
    builder.ConfigureAppConfiguration((context, config) => config.AddUserSecrets<TStartupClass>());

    // Configure config.json settings
    builder.ConfigureServices((context, services) =>
    {
      // Configure strongly-typed settings objects
      //services.Configure<SecretSettings>(context.Configuration.GetSection("Secrets"));
      //services.Configure<FileSettings>(context.Configuration.GetSection("Files"));
      //services.Configure<CommitSettings>(context.Configuration.GetSection("Commits"));
    });

    return builder;
  }
}