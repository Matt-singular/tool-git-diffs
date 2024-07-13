namespace Business.Domain.Services.RepositoryStatistics;
using System.Threading.Tasks;

public interface IGetRawCommitsDomainService
{
  public Task<GetRawCommitsDomainResponse> GetRawCommits(GetRawCommitsDomainRequest domainRequest);
}