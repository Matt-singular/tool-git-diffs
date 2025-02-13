namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;

public class GetOrgRepoAuthorStatsOctokitDomainResponse
{
  public string? RepositoryName { get; set; }
  public List<(string author, int commitCount)>? AuthorStatistics { get; set; }
}