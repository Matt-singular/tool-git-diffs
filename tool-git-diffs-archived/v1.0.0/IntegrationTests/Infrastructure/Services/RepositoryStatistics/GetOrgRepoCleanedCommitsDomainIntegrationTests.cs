namespace IntegrationTests.Infrastructure.Services.RepositoryStatistics;

using Business.Domain.Services.RepositoryStatistics;
using Business.Domain.Services.RepositoryStatistics.GetOrgRepoCleanedCommits;
using Business.Infrastructure.Services.RepositoryStatistics;
using Business.Infrastructure.Services.RepositoryStatisticsl;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Common.Shared.Config;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

/// <summary>
/// Simple Integration test that will actually call the underlying logic and API services
/// </summary>
public class GetOrgRepoCleanedCommitsDomainIntegrationTests
{
  private readonly IOptions<SecretSettings> secretSettings;
  private readonly IGetOrgRepoCleanedCommitsDomainService getOrgRepoCleanedCommitsDomainService;

  public GetOrgRepoCleanedCommitsDomainIntegrationTests()
  {
    // Configures the application configuration
    var configuration = IntegrationTestHelpers.GetBuiltApplicationConfiguration();
    secretSettings = configuration.GetOptions<SecretSettings>("Secrets");
    var commitSettings = configuration.GetOptions<CommitSettings>("Commits");

    // Configures the services
    var getAuthorisedApiClientOctokitService = Substitute.ForPartsOf<GetAuthorisedApiClientOctokitService>(secretSettings);
    var getOrgRepoCommitsOctokitService = Substitute.ForPartsOf<GetOrgRepoCommitsOctokitService>(secretSettings, getAuthorisedApiClientOctokitService);
    var getOrgRepoRawCommitsDomainService = Substitute.ForPartsOf<GetOrgRepoRawCommitsDomainService>(getOrgRepoCommitsOctokitService);
    getOrgRepoCleanedCommitsDomainService = Substitute.ForPartsOf<GetOrgRepoCleanedCommitsDomainService>(commitSettings, getOrgRepoRawCommitsDomainService);
  }

  [Fact]
  public async Task GetOrgRepoCleanedCommitsDomainIntegrationTest()
  {
    // Arrange
    var gitHubRepo = secretSettings.Value.GitHubRepositories!.First();
    var request = new GetOrgRepoCleanedCommitsDomainRequest
    {
      // Update placeholder data
      Repositories = [new IGetRepoCommitsDomainRequest.Repository { RepositoryName = gitHubRepo }],
      FromBranchOrTag = "FROM",
      ToBranchOrTag = "TO"
    };

    // Act
    var result = await getOrgRepoCleanedCommitsDomainService.GetCleanedCommits(request);

    // Assert
    result.Should().NotBeNull();
    // TOOD: more assertions
  }
}