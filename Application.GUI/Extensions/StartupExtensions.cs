namespace Application.GUI.Extensions;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Common startup extension methods
/// </summary>
[ExcludeFromCodeCoverage]
public static class StartupExtensions
{
  /// <summary>
  /// Adds the Maui pages to the service collection
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <returns>The application's service collection</returns>
  public static IServiceCollection AddMauiPages(this IServiceCollection services)
  {
    services.AddSingleton<MainPage>();

    return services;
  }
}