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
    var result = configurationBuilder.SetupCommonSharedConfiguration();

    // Assert
    result.Sources.Should().Contain(source => source.GetType().Name == "JsonConfigurationSource");
    result.Sources.Should().Contain(source => ((FileConfigurationSource)source).Path == "appsettings.json");
    result.Sources.Should().Contain(source => ((FileConfigurationSource)source).Path == "secrets.json");
  }

  [Fact]
  public void SetupCommonSharedConfigSettings_ShouldConfigureSettings()
  {
    // Arrange
    var services = new ServiceCollection();
    var configuration = Substitute.For<IConfiguration>();
    var secretsSection = Substitute.For<IConfigurationSection>();
    var commitsSection = Substitute.For<IConfigurationSection>();

    configuration.GetSection("Secrets").Returns(secretsSection);
    configuration.GetSection("Commits").Returns(commitsSection);

    // Act
    var result = services.SetupCommonSharedConfigSettings(configuration);

    var serviceProvider = result.BuildServiceProvider();
    var secretSettings = serviceProvider.GetService<IOptions<SecretSettings>>();
    var commitSettings = serviceProvider.GetService<IOptions<CommitSettings>>();

    // Assert
    secretSettings.Should().NotBeNull();
    commitSettings.Should().NotBeNull();
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