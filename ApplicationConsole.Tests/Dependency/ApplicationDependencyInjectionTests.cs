namespace ApplicationConsole.Tests.Dependency;

using ApplicationConsole.Configuration;
using ApplicationConsole.Dependency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// The unit tests for the ApplicationConsole.Dependency.ApplicationDependencyInjection
/// </summary>
public class ApplicationDependencyInjectionTests
{
  [Fact]
  public void ApplicationDependencyInjection_RegisterSingletonServices()
  {
    // Arrange
    var host = Host.CreateDefaultBuilder();
    var serviceCollection = Substitute.For<IServiceCollection>();

    // Act
    host.ConfigureServices((context, services) =>
    {
      services.AddApplicationConsoleServices();
      serviceCollection = services;
    }).Build();

    // Assert
    AssertionHelpers.AssertServiceDI<IValidateConfigurationAppService, ValidateConfigurationAppService>(serviceCollection, ServiceLifetime.Singleton, nameof(ValidateConfigurationAppService));
  }
}