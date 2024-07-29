namespace Business.Infrastructure.Services.RepositoryStatisticsl;

using Business.Domain.Services.RepositoryStatistics.GetOrgRepoCleanedCommits;
using Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;
using Common.Shared.Config;
using Microsoft.Extensions.Options;

public class GetOrgRepoCleanedCommitsDomainService(IOptions<CommitSettings> commitSettings, IGetOrgRepoRawCommitsDomainService getOrgRepoRawCommitsDomainService) : IGetOrgRepoCleanedCommitsDomainService
{
  private readonly CommitSettings commitSettings = commitSettings.Value;

  public async Task<GetOrgRepoCleanedCommitsDomainResponse> GetCleanedCommits(GetOrgRepoCleanedCommitsDomainRequest domainRequest)
  {
    throw new NotImplementedException();
  }
}