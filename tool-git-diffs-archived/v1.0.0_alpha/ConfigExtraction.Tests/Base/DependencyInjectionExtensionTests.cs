namespace ConfigExtraction.Tests.Base;

using ConfigExtraction.Base;
using ConfigExtraction.Services;
using Microsoft.Extensions.DependencyInjection;
using SharedTestDependencies;

public class DependencyInjectionExtensionTests
{
  [Fact]
  public void AddConfigExtractionServices_RegistersSingletonServices()
  {
    // Arrange
    var serviceCollection = new ServiceCollection();

    // Act
    serviceCollection.AddConfigExtractionServices();

    // Assert
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IOrchestration, Orchestration>(descriptor, ServiceLifetime.Singleton));
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IReadConfig, ReadConfig>(descriptor, ServiceLifetime.Singleton));
  }

  [Fact]
  public void AddConfigExtractionServices_RegistersScopedServices()
  {
    // Arrange
    var serviceCollection = new ServiceCollection();

    // Act
    serviceCollection.AddConfigExtractionServices();

    // Assert
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IFileServices, FileServices>(descriptor, ServiceLifetime.Scoped));
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IValidateConfig, ValidateConfig>(descriptor, ServiceLifetime.Scoped));
  }
}