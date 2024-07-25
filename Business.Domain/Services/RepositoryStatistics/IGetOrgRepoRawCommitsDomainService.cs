namespace Business.Domain.Services.RepositoryStatistics;
using System.Threading.Tasks;

public interface IGetOrgRepoRawCommitsDomainService
{
  public Task<GetOrgRepoRawCommitsDomainResponse> GetRawCommits(GetOrgRepoRawCommitsDomainRequest domainRequest);
}