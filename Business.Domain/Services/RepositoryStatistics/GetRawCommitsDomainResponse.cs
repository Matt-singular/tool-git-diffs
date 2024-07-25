namespace Business.Domain.Services.RepositoryStatistics;

using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public class GetRawCommitsDomainResponse : List<GetRawCommitsDomainResponse.RepositoryCommits>
{
  public class RepositoryCommits : GetOrgRepoCommitsOctokitDomainResponse.CommitDetails
  {
    public required string RepositoryName { get; set; }
  }
}