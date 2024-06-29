namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;

public interface IGetAuthorRepoStatsOctokitService
{
  public Task<GetAuthorRepoStatsOctokitResponse> ProcessAsync(GetAuthorRepoStatsOctokitRequest request);
}