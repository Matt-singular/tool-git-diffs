namespace Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public class GetRepositoryStatisticsOctokitDomainResponse
{
  public required string RepositoryName { get; set; }
  public required List<CommitDetails> Commits { get; set; }

  public class CommitDetails
  {
    public required string Hash { get; set; }
    public required string AuthorName { get; set; }
    public required string AuthorEmail { get; set; }
    public required string Message { get; set; }
    public required DateTime DateOfCommit { get; set; }
    public required bool IsMergeCommit { get; set; }
  }
}