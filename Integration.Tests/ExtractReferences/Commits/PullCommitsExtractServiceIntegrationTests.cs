namespace Integration.Tests.ExtractReferences.Commits;

using FluentAssertions;
using global::ExtractReferences.Authorisation;
using global::ExtractReferences.Commits;
using NSubstitute;
using TestHelpers;

public class PullCommitsExtractServiceIntegrationTests
{
  [Fact]
  public async Task ExtractReferences_PullCommitsExtractService_InvalidReference_ShouldThrowError()
  {
    // Arrange
    var mockedSecretSettings = ConfigHelpers.MockSecretSettings();
    var gitHubAuthService = Substitute.ForPartsOf<GitHubAuthExtractService>(mockedSecretSettings);
    var pullCommitsExtractService = Substitute.ForPartsOf<PullCommitsExtractService>(mockedSecretSettings, gitHubAuthService);

    // Act
    var repositoryName = mockedSecretSettings.Value.GitHubRepositories.First();
    Func<Task> act = async () => await pullCommitsExtractService.ProcessAsync(repositoryName, "unknown", "12.2.1");

    // Assert
    await act.Should().ThrowAsync<Octokit.NotFoundException>();
  }

  [Fact]
  public async Task ExtractReferences_PullCommitsExtractService_ValidReferences()
  {
    // Arrange
    var mockedSecretSettings = ConfigHelpers.MockSecretSettings();
    var gitHubAuthService = Substitute.ForPartsOf<GitHubAuthExtractService>(mockedSecretSettings);
    var pullCommitsExtractService = Substitute.ForPartsOf<PullCommitsExtractService>(mockedSecretSettings, gitHubAuthService);

    // Act
    var repositoryName = mockedSecretSettings.Value.GitHubRepositories.First();
    var result = await pullCommitsExtractService.ProcessAsync(repositoryName, "12.2.0", "12.2.1");

    // Assert
    result.CommitDetails.Should().NotBeEmpty();
  }
}
