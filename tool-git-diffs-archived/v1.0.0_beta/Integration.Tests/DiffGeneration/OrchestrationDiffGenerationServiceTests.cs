namespace Integration.Tests.DiffGeneration;

using Configuration.Settings;
using FluentAssertions;
using global::DiffGeneration;
using global::ExtractReferences.Authorisation;
using global::ExtractReferences.Commits;
using Microsoft.Extensions.Options;
using NSubstitute;
using TestHelpers;

public class OrchestrationDiffGenerationServiceTests
{
  public OrchestrationDiffGenerationService SetupOrchestrationDiffGenerationService(IOptions<SecretSettings> mockedSecretSettings)
  {
    var gitHubAuthService = Substitute.ForPartsOf<GitHubAuthExtractService>(mockedSecretSettings);
    var pullCommitsExtractService = Substitute.ForPartsOf<PullCommitsExtractService>(gitHubAuthService);
    var mockedCommitSettings = ConfigHelpers.MockCommitSettings();
    var orchestrationDiffGeneration = Substitute.ForPartsOf<OrchestrationDiffGenerationService>(mockedCommitSettings, mockedSecretSettings, pullCommitsExtractService);

    return orchestrationDiffGeneration;
  }

  [Fact]
  public async Task DiffGeneration_PullRawDiffs_SingleRepository()
  {
    // Arrange
    var mockedSecretSettings = ConfigHelpers.MockSecretSettings();
    var repo = mockedSecretSettings.Value.GitHubRepositories.First();
    mockedSecretSettings.Value.GitHubRepositories = [repo];
    var orchestrationDiffGeneration = SetupOrchestrationDiffGenerationService(mockedSecretSettings);

    // Act
    var rawDiffs = await orchestrationDiffGeneration.PullRawDiffs(IntegrationHelpers.ValidFromTagReference, IntegrationHelpers.ValidToTagReference);

    // Assert
    rawDiffs.Should().NotBeEmpty();
  }

  [Fact]
  public async Task DiffGeneration_PullRawDiffs_MultipleRepositories()
  {
    // Arrange
    var mockedSecretSettings = ConfigHelpers.MockSecretSettings();
    var repo1 = mockedSecretSettings.Value.GitHubRepositories.First();
    var repo2 = mockedSecretSettings.Value.GitHubRepositories.Last();
    mockedSecretSettings.Value.GitHubRepositories = [repo1, repo2];
    var orchestrationDiffGeneration = SetupOrchestrationDiffGenerationService(mockedSecretSettings);

    // Act
    var rawDiffs = await orchestrationDiffGeneration.PullRawDiffs(IntegrationHelpers.ValidFromTagReference, IntegrationHelpers.ValidToTagReference);

    // Assert
    rawDiffs.Should().NotBeEmpty();
  }
}