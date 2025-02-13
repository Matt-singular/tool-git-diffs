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
    var getOrgRepoRawCommitsDomainResponse = await GetRawCommitsFromOctokitAsync(domainRequest).ConfigureAwait(false);

    throw new NotImplementedException();
  }

  private async Task<GetOrgRepoRawCommitsDomainResponse> GetRawCommitsFromOctokitAsync(GetOrgRepoCleanedCommitsDomainRequest domainRequest)
  {
    var getOrgRepoRawCommitsDomainRequest = new GetOrgRepoRawCommitsDomainRequest
    {
      Repositories = domainRequest.Repositories,
      FromBranchOrTag = domainRequest.FromBranchOrTag,
      ToBranchOrTag = domainRequest.ToBranchOrTag
    };

    var getOrgRepoRawCommitsDomainTask = getOrgRepoRawCommitsDomainService.GetRawCommits(getOrgRepoRawCommitsDomainRequest);
    var getOrgRepoRawCommitsDomainResponse = await getOrgRepoRawCommitsDomainTask.ConfigureAwait(false);

    return getOrgRepoRawCommitsDomainResponse;
  }
}