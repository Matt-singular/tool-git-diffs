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
    var pullCommitsExtractService = Substitute.ForPartsOf<PullCommitsExtractService>(gitHubAuthService);

    // Act
    var repositoryName = mockedSecretSettings.Value.GitHubRepositories.First();
    var act = async () => await pullCommitsExtractService.ProcessAsync(repositoryName, IntegrationHelpers.NonExistantReference, IntegrationHelpers.ValidToTagReference);

    // Assert
    await act.Should().ThrowAsync<Octokit.NotFoundException>();
  }

  [Fact]
  public async Task ExtractReferences_PullCommitsExtractService_ValidReferences()
  {
    // Arrange
    var mockedSecretSettings = ConfigHelpers.MockSecretSettings();
    var gitHubAuthService = Substitute.ForPartsOf<GitHubAuthExtractService>(mockedSecretSettings);
    var pullCommitsExtractService = Substitute.ForPartsOf<PullCommitsExtractService>(gitHubAuthService);

    // Act
    var repositoryName = mockedSecretSettings.Value.GitHubRepositories.First();
    var result = await pullCommitsExtractService.ProcessAsync(repositoryName, IntegrationHelpers.ValidFromTagReference, IntegrationHelpers.ValidToTagReference);

    // Assert
    result.CommitDetails.Should().NotBeEmpty();
  }
}
