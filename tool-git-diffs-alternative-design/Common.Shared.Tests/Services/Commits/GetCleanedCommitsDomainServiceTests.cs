namespace Common.Shared.Tests.Services.Commits;

using Common.Shared.Services.Commits.GetCleanedCommits;
using FluentAssertions;
using TestHelpers.Mocks.Commits;
using TestHelpers.Mocks.Config;

/// <summary>
/// Unit tests for <see cref="GetCleanedCommitsDomainService"/>
/// </summary>
public class GetCleanedCommitsDomainServiceTests
{
  [Fact]
  public void Process_ShouldReturnCleanedCommits()
  {
    // Arrange
    var mockedCommitSettings = MockedCommitSettings.CreateOptionsScenarioA();
    var mockedRawCommits = MockedGetRawCommitsDomainResponse.CreateGetRawCommitsDomainResponseScenarioA();
    var getCleanedCommitsDomain = new GetCleanedCommitsDomainService(mockedCommitSettings);

    // Act
    var domainResponse = getCleanedCommitsDomain.Process(mockedRawCommits);

    // Assert
    var expectedResponse = MockedGetCleanedCommitsDomainResponse.CreateGetCleanedCommitsDomainResponseScenarioA();
    domainResponse.Should().NotBeNullOrEmpty();
    domainResponse.Should().BeEquivalentTo(expectedResponse);
  }
}