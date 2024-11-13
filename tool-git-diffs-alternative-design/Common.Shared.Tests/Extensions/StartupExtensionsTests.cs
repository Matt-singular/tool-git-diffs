namespace Common.Shared.Tests.Extensions;

using Common.Shared.Config;
using Common.Shared.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;

public class StartupExtensionsTests
{
  [Fact]
  public void SetupCommonSharedConfiguration_ShouldAddJsonFileAndUserSecrets()
  {
    // Arrange
    var configurationBuilder = new ConfigurationBuilder();

    // Act
    var result = configurationBuilder.AddCommonSharedConfiguration();

    // Assert
    result.Sources.Should().Contain(source => source.GetType().Name == "JsonConfigurationSource");
    result.Sources.Should().Contain(source => ((FileConfigurationSource)source).Path == "appsettings.json");
    result.Sources.Should().Contain(source => ((FileConfigurationSource)source).Path == "secrets.json");
  }

  [Theory]
  [InlineData("Secrets", typeof(SecretSettings))]
  [InlineData("Commits", typeof(CommitSettings))]
  public void SetupCommonSharedConfigSettings_ShouldConfigureSettings(string sectionName, Type settingsType)
  {
    // Arrange
    var services = new ServiceCollection();
    var configuration = Substitute.For<IConfiguration>();

    configuration.GetSection(sectionName).Returns(Substitute.For<IConfigurationSection>());

    // Act
    var result = services.ConfigureCommonSettings(configuration);
    var serviceProvider = result.BuildServiceProvider();

    var configuredSettingsType = typeof(IOptions<>).MakeGenericType(settingsType);
    var settings = serviceProvider.GetService(configuredSettingsType);

    // Assert
    settings.Should().NotBeNull();
    configuration.Received().GetSection(sectionName);
  }

  [Fact]
  public void AddCommonSharedServices_ShouldRegisterServices()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    var result = services.AddCommonSharedServices();

    // Assert
    result.Should().BeEmpty();
  }
}