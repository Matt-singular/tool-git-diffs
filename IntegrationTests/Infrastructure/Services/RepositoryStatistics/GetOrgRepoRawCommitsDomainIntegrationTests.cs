namespace IntegrationTests.Infrastructure.Services.RepositoryStatistics;

using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;
using Business.Infrastructure.Services.RepositoryStatistics;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Common.Shared.Config;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

/// <summary>
/// Simple Integration test that will actually call the underlying logic and API services
/// </summary>
public class GetOrgRepoRawCommitsDomainIntegrationTests
{
  private readonly IOptions<SecretSettings> secretSettings;
  private IGetOrgRepoRawCommitsDomainService getOrgRepoRawCommitsDomainService;

  public GetOrgRepoRawCommitsDomainIntegrationTests()
  {
    // Configures the application configuration
    var configuration = IntegrationTestHelpers.GetBuiltApplicationConfiguration();
    secretSettings = configuration.GetOptions<SecretSettings>("Secrets");

    // Configures the services
    var getAuthorisedApiClientOctokitService = Substitute.ForPartsOf<GetAuthorisedApiClientOctokitService>(secretSettings);
    var getOrgRepoCommitsOctokitService = Substitute.ForPartsOf<GetOrgRepoCommitsOctokitService>(secretSettings, getAuthorisedApiClientOctokitService);
    getOrgRepoRawCommitsDomainService = Substitute.ForPartsOf<GetOrgRepoRawCommitsDomainService>(getOrgRepoCommitsOctokitService);
  }

  [Fact]
  public async Task GetOrgRepoRawCommitsDomainIntegrationTest()
  {
    // Arrange
    var gitHubRepo = secretSettings.Value.GitHubRepositories!.First();
    var request = new GetOrgRepoRawCommitsDomainRequest
    {
      // Update placeholder data
      Repositories = [new GetOrgRepoRawCommitsDomainRequest.Repository { RepositoryName = gitHubRepo }],
      FromBranchOrTag = "FROM",
      ToBranchOrTag = "TO",
    };

    // Act
    var result = await getOrgRepoRawCommitsDomainService.GetRawCommits(request);

    // Assert
    result.Should().NotBeNull();
    result.Should().NotBeEmpty();
  }
}