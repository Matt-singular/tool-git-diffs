namespace IntegrationTests.Common.Shared.Commits;

using FluentAssertions;
using global::Common.Shared.Services.Commits.GetRawCommits;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Integration tests for <see cref="GetRawCommitsDomainService"/>
/// </summary>
public class GetRawCommitsIntegrationTests
{
  [Fact]
  public async Task ProcessAsync_ShouldReturnRawCommits()
  {
    // Arrange
    var serviceProvider = IntegrationTestHelpers.RunStartupExtensions().BuildServiceProvider();
    var service = serviceProvider.GetRequiredService<IGetRawCommitsDomainService>();

    // Act
    var result = await service.ProcessAsync(740670625, fromReference: "POC", toReference: "main");

    // Assert
    result.Should().NotBeNull();
  }
}
