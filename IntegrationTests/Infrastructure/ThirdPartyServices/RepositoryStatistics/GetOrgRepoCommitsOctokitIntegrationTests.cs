namespace IntegrationTests.Infrastructure.ThirdPartyServices.RepositoryStatistics;
using System.Linq;
using System.Threading.Tasks;
using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;
using Common.Shared.Config;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

/// <summary>
/// Simple Integration test that will actually call the underlying logic and API services
/// </summary>
public class GetOrgRepoCommitsOctokitIntegrationTests
{
  private readonly IGetOrgRepoCommitsOctokitService getOrgRepoCommitsOctokitService;
  private readonly IOptions<SecretSettings> secretSettings;

  public GetOrgRepoCommitsOctokitIntegrationTests()
  {
    // Configures the application configuration
    var configuration = IntegrationTestHelpers.GetBuiltApplicationConfiguration();
    secretSettings = configuration.GetOptions<SecretSettings>("Secrets");

    // Configures the services
    var getAuthorisedApiClientOctokitService = Substitute.ForPartsOf<GetAuthorisedApiClientOctokitService>(secretSettings);
    getOrgRepoCommitsOctokitService = Substitute.ForPartsOf<GetOrgRepoCommitsOctokitService>(secretSettings, getAuthorisedApiClientOctokitService);
  }

  [Fact]
  public async Task GetOrgRepoCommitsOctokitIntegrationTest()
  {
    // Arrange
    var gitHubRepo = secretSettings.Value.GitHubRepositories!.First();
    var request = new GetOrgRepoCommitsOctokitDomainRequest
    {
      // Update placeholder data
      RepositoryName = gitHubRepo,
      FromBranchOrTag = "FROM",
      ToBranchOrTag = "TO",
      ExcludeMergeCommits = true,
    };

    // Act
    var result = await getOrgRepoCommitsOctokitService.ProcessAsync(request);

    // Assert
    result.Should().NotBeNull();
    result.RepositoryName.Should().Be(gitHubRepo);
    result.Commits.Should().NotBeNull();
    result.Commits.Should().NotBeEmpty();
    result.Commits.Count.Should().BeGreaterThan(0);
  }
}