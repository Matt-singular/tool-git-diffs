namespace Business.Domain.Services.RepositoryStatistics;

using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public class GetRawCommitsDomainResponse : List<GetRawCommitsDomainResponse.RepositoryStatistics>
{
  public class RepositoryStatistics : GetOrgRepoCommitsOctokitDomainResponse.CommitDetails
  {
    public required string RepositoryName { get; set; }
  }
}