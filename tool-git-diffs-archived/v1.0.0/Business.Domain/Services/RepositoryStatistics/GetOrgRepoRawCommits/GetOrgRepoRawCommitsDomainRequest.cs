namespace Business.Domain.Services.RepositoryStatistics.GetOrgRepoRawCommits;

public class GetOrgRepoRawCommitsDomainRequest : IGetRepoCommitsDomainRequest
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