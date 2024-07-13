namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;

public interface IGetAuthorRepoStatsOctokitService
{
  public Task<GetAuthorRepoStatsOctokitDomainResponse> ProcessAsync(GetAuthorRepoStatsOctokitDomainRequest request);
}