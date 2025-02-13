namespace Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;

using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public class GetOrgRepoRawCommitsDomainResponse : List<GetOrgRepoRawCommitsDomainResponse.RepositoryCommits>
{
    public class RepositoryCommits : GetOrgRepoCommitsOctokitDomainResponse.CommitDetails
    {
        public required string RepositoryName { get; set; }
    }
}