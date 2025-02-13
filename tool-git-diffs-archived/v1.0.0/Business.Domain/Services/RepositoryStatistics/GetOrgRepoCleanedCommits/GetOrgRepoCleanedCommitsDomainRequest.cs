namespace Business.Domain.Services.RepositoryStatistics.GetOrgRepoCleanedCommits;

using System.Collections.Generic;

public class GetOrgRepoCleanedCommitsDomainRequest : IGetRepoCommitsDomainRequest
{
  public required List<IGetRepoCommitsDomainRequest.Repository> Repositories { get; set; }
  public string? FromBranchOrTag { get; set; }
  public string? ToBranchOrTag { get; set; }

  public bool ValidateModel()
  {
    var isValid = IGetRepoCommitsDomainRequest.ValidateModel(this);

    return isValid;
  }
}