﻿namespace Business.Domain.Tests;

using Business.Domain;
using Business.Models.Commits.GetCleanedCommits;
using Business.Models.Commits.GetRawCommits;
using Business.Models.Reports.GetCleanedExcelReport;
using Business.Models.Reports.GetRawExcelReport;
using Business.Models.Repositories.GetRepositoryDetail;
using Business.Models.Repositories.GetRepositoryList;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestHelpers;

/// <summary>
/// Unit tests for <see cref="StartupExtensions"/>
/// </summary>
public class StartupExtensionsTests
{
  private readonly ServiceProvider serviceProvider;

  public StartupExtensionsTests()
  {
    // Arrange
    var services = new ServiceCollection();
    services.MockConfigurationSettings();
    services.AddBusinessDomainServices();

    // Act
    this.serviceProvider = services.BuildServiceProvider();
  }

  [Theory]
  [InlineData(typeof(IGetCleanedCommits))]
  [InlineData(typeof(IGetRawCommits))]
  [InlineData(typeof(IGetCleanedExcelReport))]
  [InlineData(typeof(IGetRawExcelReport))]
  [InlineData(typeof(IGetRepositoryDetail))]
  [InlineData(typeof(IGetRepositoryList))]
  public void AddBusinessDomainServices_ShouldRegisterAllDomainServices(Type serviceInterface)
  {
    // Assert
    serviceProvider.GetService(serviceInterface).Should().NotBeNull();
  }
}