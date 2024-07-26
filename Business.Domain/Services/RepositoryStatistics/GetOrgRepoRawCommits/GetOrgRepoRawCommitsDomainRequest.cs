namespace Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;

public class GetOrgRepoRawCommitsDomainRequest
{
    /// <summary>
    /// The repositories to pull data for
    /// </summary>
    public required List<Repository> Repositories { get; set; }

    /// <summary>
    /// The from git reference for all of the repositories to use
    /// </summary>
    public string? FromBranchOrTag { get; set; }

    /// <summary>
    /// The to git reference for all of the repositories to use
    /// </summary>
    public string? ToBranchOrTag { get; set; }

    public class Repository
    {
        /// <summary>
        /// The name of the repository
        /// </summary>
        public required string RepositoryName { get; set; }

        /// <summary>
        /// The from git reference for the specified repository to use
        /// </summary>
        public string? FromBranchOrTag { get; set; }

        /// <summary>
        /// The to git reference for the specified repository to use
        /// </summary>
        public string? ToBranchOrTag { get; set; }
    }

    public bool ValidateModel()
    {
        // Checks if global git references were supplied
        var model = this;
        var hasGlobalFromBranchOrTag = !string.IsNullOrEmpty(model.FromBranchOrTag);
        var hasGlobalToBranchOrTag = !string.IsNullOrEmpty(model.ToBranchOrTag);

        // Checks each of the underlying repositories
        var isValidRequest = model.Repositories.All(repo =>
        {
            var isValidRepositoryName = !string.IsNullOrEmpty(repo.RepositoryName);
            var isValidFromBranchOrTag = hasGlobalFromBranchOrTag || !string.IsNullOrEmpty(repo.FromBranchOrTag);
            var isValidToBranchOrTag = hasGlobalToBranchOrTag || !string.IsNullOrEmpty(repo.ToBranchOrTag);
            return isValidRepositoryName && isValidFromBranchOrTag && isValidToBranchOrTag;
        });

        // The request is only valid if 
        return isValidRequest;
    }
}