namespace TestHelpers;

using Business.Domain;
using Common.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Some helper methods for integration tests
/// </summary>
public static class IntegrationTestHelpers
{
  /// <summary>
  /// Run the different project Startup extensions to register configuration & services
  /// </summary>
  /// <returns>The configured service collection</returns>
  public static IServiceCollection RunStartupExtensions()
  {
    var configuration = new ConfigurationBuilder().AddCommonSharedConfiguration().Build();

    var serviceCollection = new ServiceCollection()
      .ConfigureCommonSettings(configuration)
      .AddCommonSharedServices()
      .AddBusinessDomainServices();

    return serviceCollection;
  }
}