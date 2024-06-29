namespace IntegrationTests.Infrastructure.ThirdPartyServices.Statistics;

using Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;
using Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Statistics;
using Common.Shared.Config;
using FluentAssertions;
using IntegrationTests;
using Microsoft.Extensions.Options;
using NSubstitute;

public class GetAuthorRepoStatsOctokitTests
{
  private readonly IGetAuthorRepoStatsOctokitService getRepositoryStatisticsOctokitService;
  private readonly IOptions<SecretSettings> secretSettings;

  public GetAuthorRepoStatsOctokitTests()
  {
    // Configures the application configuration
    var configuration = IntegrationTestHelpers.GetBuiltApplicationConfiguration();
    secretSettings = configuration.GetOptions<SecretSettings>("Secrets");

    // Configures the services
    var getAuthorisedApiClientOctokitService = Substitute.ForPartsOf<GetAuthorisedApiClientOctokitService>(secretSettings);
    getRepositoryStatisticsOctokitService = Substitute.ForPartsOf<GetAuthorRepoStatsOctokitService>(secretSettings, getAuthorisedApiClientOctokitService);
  }

  [Fact]
  public async Task GetRepositoryStatisticsOctokitIntegrationTest()
  {
    // Arrange
    var gitHubRepo = secretSettings.Value.GitHubRepositories!.First();
    var request = new GetAuthorRepoStatsOctokitRequest
    {
      Author = null,
      RepositoryName = gitHubRepo,
      Since = DateTime.Now.AddDays(-30),
      Until = DateTime.Now
    };

    // Act
    var result = await getRepositoryStatisticsOctokitService.ProcessAsync(request);

    // Assert
    result.Should().NotBeNull();
    result.RepositoryName.Should().Be(gitHubRepo);
    result.AuthorStatistics.Should().NotBeNull();
    result.AuthorStatistics.Should().NotBeEmpty();
    result.AuthorStatistics!.Sum(stats => stats.commitCount).Should().BeGreaterThan(0);
  }
}