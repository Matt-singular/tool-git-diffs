namespace Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;
using System.Threading.Tasks;

public interface IGetOrgRepoRawCommitsDomainService
{
    public Task<GetOrgRepoRawCommitsDomainResponse> GetRawCommits(GetOrgRepoRawCommitsDomainRequest domainRequest);
}