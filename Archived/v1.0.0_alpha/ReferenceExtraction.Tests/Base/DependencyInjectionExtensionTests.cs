namespace ReferenceExtraction.Tests.Base;

using Microsoft.Extensions.DependencyInjection;
using ReferenceExtraction.Base;
using ReferenceExtraction.Services;
using SharedTestDependencies;

public class DependencyInjectionExtensionTests
{
  [Fact]
  public void AddReferenceExtractionServices_RegistersSingletonServices()
  {
    // Arrange
    var serviceCollection = new ServiceCollection();

    // Act
    serviceCollection.AddReferenceExtractionServices();

    // Assert
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IOrchestration, Orchestration>(descriptor, ServiceLifetime.Singleton));
  }

  [Fact]
  public void AddReferenceExtractionServices_RegistersScopedServices()
  {
    // Arrange
    var serviceCollection = new ServiceCollection();

    // Act
    serviceCollection.AddReferenceExtractionServices();

    // Assert
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IGenerateRegexes, GenerateRegexes>(descriptor, ServiceLifetime.Scoped));
    serviceCollection.Should().ContainSingle(descriptor => Helpers.ValidDependencyType<IExtractReferences, ExtractReferences>(descriptor, ServiceLifetime.Scoped));
  }
}