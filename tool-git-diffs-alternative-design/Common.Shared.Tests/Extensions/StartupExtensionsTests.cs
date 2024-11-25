namespace Common.Shared.Tests.Extensions;

using System;
using Common.Shared.Config;
using Common.Shared.Extensions;
using Common.Shared.Services.Commits.GetRawCommits;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;

/// <summary>
/// Unit tests for <see cref="StartupExtensions"/>
/// </summary>
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

  [Theory]
  [InlineData(typeof(IGetRawCommitsDomainService))]
  public void AddCommonSharedServices_ShouldRegisterAllCommonServices(Type serviceInterface)
  {
    // Arrange
    var services = new ServiceCollection();
    services.MockConfigurationSettings();
    services.AddCommonSharedServices();

    // Act
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    serviceProvider.GetService(serviceInterface).Should().NotBeNull();
  }
}