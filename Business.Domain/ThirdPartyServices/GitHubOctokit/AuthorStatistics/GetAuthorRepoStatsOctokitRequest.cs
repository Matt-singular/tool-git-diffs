namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;

using Octokit;

public class GetAuthorRepoStatsOctokitRequest : CommitRequest
{
  public required string RepositoryName { get; set; }
  public required new string? Author { get; set; }
  public new DateTime Since { get; set; }
  public new DateTime Until { get; set; }
}