namespace Business.Domain.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Business.Domain startup extension methods
/// </summary>
[ExcludeFromCodeCoverage]
public static class StartupExtensions
{
  /// <summary>
  /// Adds Business.Domain services to the service collection
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <returns>The service collection with Business.Domain services registered</returns>
  public static IServiceCollection AddBusinessDomainServices(this IServiceCollection services)
  {
    // Register Business.Domain services

    return services;
  }
}