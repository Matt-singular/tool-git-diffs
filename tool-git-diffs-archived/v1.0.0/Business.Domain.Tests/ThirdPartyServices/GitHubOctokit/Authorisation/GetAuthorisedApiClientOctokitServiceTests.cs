namespace Business.Domain.Tests.ThirdPartyServices.GitHubOctokit.Authorisation;

using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using NSubstitute;

/// <summary>
/// Testing the GetAuthorisedApiClientOctokitService
/// </summary>
public class GetAuthorisedApiClientOctokitServiceTests
{
  private readonly IGetAuthorisedApiClientOctokitService getAuthorisedApiClientOctokitService;

  public GetAuthorisedApiClientOctokitServiceTests()
  {
    this.getAuthorisedApiClientOctokitService = Substitute.ForPartsOf<IGetAuthorisedApiClientOctokitService>();
  }



  /*
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
  */
}