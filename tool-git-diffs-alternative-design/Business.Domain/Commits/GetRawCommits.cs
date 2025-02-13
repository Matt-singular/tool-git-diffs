namespace Business.Domain.Commits;

using System.Threading.Tasks;
using Business.Models.Commits.GetRawCommits;
using Common.Shared.Models.Commits;
using Common.Shared.Models.Exceptions;
using Common.Shared.Services.Commits.GetRawCommits;
using static Business.Models.Commits.GetRawCommits.GetRawCommitsRequest;

/// <inheritdoc cref="IGetRawCommits"/>
public class GetRawCommits(IGetRawCommitsDomainService getRawCommitsDomainService) : IGetRawCommits
{
  private readonly IGetRawCommitsDomainService getRawCommitsDomainService = getRawCommitsDomainService;

  /// <inheritdoc/>
  public async Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request)
  {
    BadRequestException.ThrowIfNullOrEmpty(request.Repositories, nameof(request.Repositories));

    var getRawCommitsFromDomainTasks = request.Repositories!.Select(GetRawCommitsFromDomainAsync).ToList();

    var getRawCommitsFromDomainResponses = await Task.WhenAll(getRawCommitsFromDomainTasks).ConfigureAwait(false);

    var businessResponse = MapGetRawCommitsResponse([.. getRawCommitsFromDomainResponses]);

    return businessResponse;
  }

  private async Task<(RepositoryLookupCriteria criteria, List<RawCommitDetails> commits)> GetRawCommitsFromDomainAsync(
    RepositoryLookupCriteria repositoryLookupCriteria)
  {
    // Perform a raw commits lookup against the repository using either it's id or name & owner
    var repositoryId = repositoryLookupCriteria.RepositoryId;
    var repositoryName = repositoryLookupCriteria.RepositoryName;
    var repositoryOwner = repositoryLookupCriteria.RepositoryOwner;
    var fromReference = repositoryLookupCriteria.FromReference;
    var toReference = repositoryLookupCriteria.ToReference;

    var rawCommitsLookupTask = repositoryId is not null
      ? this.getRawCommitsDomainService.ProcessAsync(repositoryId.Value, fromReference, toReference)
      : this.getRawCommitsDomainService.ProcessAsync(repositoryName!, repositoryOwner!, fromReference, toReference);

    var rawCommitsLookupResponse = await rawCommitsLookupTask.ConfigureAwait(false);

    return (repositoryLookupCriteria, rawCommitsLookupResponse);
  }

  private static GetRawCommitsResponse MapGetRawCommitsResponse(
    List<(RepositoryLookupCriteria criteria, List<RawCommitDetails> commits)> getRawCommitsFromDomainResponses)
  {
    // Flatten the commits into a one-dimensional list
    var rawCommits = FlattenRawCommitsTwoDimensionalList(getRawCommitsFromDomainResponses);

    var repositoryRawCommits = getRawCommitsFromDomainResponses
      .GroupBy(groupSelector => groupSelector.criteria.UniqueReference)
      .Select(repositoryGroup => new GetRawCommitsResponse.RepositoryRawCommitsDetail
      {
        // Repository-level commits
        RepositoryName = repositoryGroup.Key,
        FromReference = repositoryGroup.First().criteria.FromReference,
        ToReference = repositoryGroup.First().criteria.ToReference,
        RawCommits = FlattenRawCommitsTwoDimensionalList([.. repositoryGroup])
      })
      .ToList();

    return new(repositoryRawCommits);
  }

  private static List<RawCommitDetails> FlattenRawCommitsTwoDimensionalList(
    List<(RepositoryLookupCriteria criteria, List<RawCommitDetails> commits)> getRawCommitsFromDomainResponses)
  {
    return getRawCommitsFromDomainResponses
      .SelectMany(getRawCommitsFromDomainResponse => getRawCommitsFromDomainResponse.commits)
      .ToList();
  }
}