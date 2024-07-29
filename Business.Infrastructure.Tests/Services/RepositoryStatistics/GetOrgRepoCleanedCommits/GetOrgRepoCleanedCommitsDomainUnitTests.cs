namespace Business.Infrastructure.Tests.Services.RepositoryStatistics.GetOrgRepoCleanedCommits;

using Business.Domain.Services.RepositoryStatistics.GetOrgRepoCleanedCommits;
using Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;
using Business.Infrastructure.Services.RepositoryStatisticsl;
using Common.Shared.Config;
using FluentAssertions;
using NSubstitute;

/// <summary>
/// Unit tests focused on the GetOrgRepoCleanedCommits Domain service logic
/// </summary>
public class GetOrgRepoCleanedCommitsDomainUnitTests
{
  private readonly string JsonPath = $"{AppContext.BaseDirectory}\\Services\\RepositoryStatistics\\GetOrgRepoCleanedCommits";
  private readonly IGetOrgRepoCleanedCommitsDomainService getOrgRepoCleanedCommitsDomainService;

  public GetOrgRepoCleanedCommitsDomainUnitTests()
  {
    // Mock application configuration
    var mockedCommitOptions = BusinessInfrastructureTestHelper.MockOptionSettings(new CommitSettings
    {
      Patterns = [], // List<CommitPattern>
      SortOrder = [], // List<string>
      OrderBy = ""
    });

    // Mock service
    var mockedGetOrgRepoRawCommitsResponse = BusinessInfrastructureTestHelper.ParseJsonFile<GetOrgRepoRawCommitsDomainResponse>(JsonPath, "getOrgRepoRawCommitsFeatReferencesPayload.json");

    var mockedGetOrgRepoRawCommits = BusinessInfrastructureTestHelper
      .MockDomainService<IGetOrgRepoRawCommitsDomainService>()
      .MockDomainServiceResponse<IGetOrgRepoRawCommitsDomainService, GetOrgRepoRawCommitsDomainRequest, GetOrgRepoRawCommitsDomainResponse>(service => service.GetRawCommits, mockedGetOrgRepoRawCommitsResponse!);

    // Configure the service to test
    getOrgRepoCleanedCommitsDomainService = Substitute.ForPartsOf<GetOrgRepoCleanedCommitsDomainService>(mockedCommitOptions, mockedGetOrgRepoRawCommits);
  }


  [Fact]
  public async Task GetOrgRepoCleanedCommitsWithValidFeatureReferencesOnly()
  {
    // Arrange
    var request = new GetOrgRepoCleanedCommitsDomainRequest
    {
      // TODO: update request once it has been set up
    };

    // Act
    var result = await getOrgRepoCleanedCommitsDomainService.GetCleanedCommits(request);

    // Assert
    result.Should().NotBeNull();
    // TOOD: other assertions
  }
}