namespace Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;

public static class GetOrgRepoRawCommitsDomainExtensions
{
    public static (string FromBranchOrTag, string ToBranchOrTag) GetGitReference(this GetOrgRepoRawCommitsDomainRequest.Repository repository, GetOrgRepoRawCommitsDomainRequest domainRequest)
    {
        // Gets the appropriate from branch/tag
        var fromBranchOrTag = domainRequest.FromBranchOrTag;
        if (!string.IsNullOrEmpty(repository.FromBranchOrTag))
        {
            // Gets the repository-level from branch/tag
            fromBranchOrTag = repository.FromBranchOrTag;
        }

        // Gets the appropriate to branch/tag
        var toBranchOrTag = domainRequest.ToBranchOrTag;
        if (!string.IsNullOrEmpty(repository.ToBranchOrTag))
        {
            // Gets the repository-level to branch/tag
            toBranchOrTag = repository.FromBranchOrTag;
        }

        return (fromBranchOrTag!, toBranchOrTag!);
    }
}