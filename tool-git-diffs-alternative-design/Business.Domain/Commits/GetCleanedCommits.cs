namespace Business.Domain.Commits;

using System.Threading.Tasks;
using Business.Models.Commits.GetCleanedCommits;
using Business.Models.Commits.GetRawCommits;
using Common.Shared.Models.Exceptions;
using Common.Shared.Services.Commits.GetCleanedCommits;
using static Business.Models.Commits.GetCleanedCommits.GetCleanedCommitsResponse;

/// <inheritdoc cref="IGetCleanedCommits"/>
public class GetCleanedCommits(IGetCleanedCommitsDomainService getCleanedCommitsDomainService) : IGetCleanedCommits
{
  private readonly IGetCleanedCommitsDomainService getCleanedCommitsDomainService = getCleanedCommitsDomainService;

  /// <inheritdoc/>
  public async Task<GetCleanedCommitsResponse> ProcessAsync(GetCleanedCommitsRequest request)
  {
    BadRequestException.ThrowIfNullOrEmpty(request.RepositoryRawCommits, nameof(request.RepositoryRawCommits));

    var getCleanedCommitsFromDomainTasks = request.RepositoryRawCommits.Select(GetCleanedCommitsFromDomainAsync).ToList();

    var getCleanedCommitsFromDomainResponses = await Task.WhenAll(getCleanedCommitsFromDomainTasks).ConfigureAwait(false);

    var businessResponse = MapGetCleanedCommitsResponse(getCleanedCommitsFromDomainResponses);

    return businessResponse;
  }

  private Task<RepositoryCleanedCommitsDetail> GetCleanedCommitsFromDomainAsync(
    GetRawCommitsResponse.RepositoryRawCommitsDetail repositoryRawCommitsDetail)
  {
    var getCleanedCommitsDomainTask = Task.Run(() =>
    {
      var cleanedCommitsLookup = this.getCleanedCommitsDomainService.Process(repositoryRawCommitsDetail.RawCommits);

      return new RepositoryCleanedCommitsDetail
      {
        RepositoryName = repositoryRawCommitsDetail.RepositoryName,
        FromReference = repositoryRawCommitsDetail.FromReference,
        ToReference = repositoryRawCommitsDetail.ToReference,
        CleanedCommits = cleanedCommitsLookup
      };
    });

    return getCleanedCommitsDomainTask;
  }

  private static GetCleanedCommitsResponse MapGetCleanedCommitsResponse(
    RepositoryCleanedCommitsDetail[] getCleanedCommitsFromDomainResponses)
  {
    return new([.. getCleanedCommitsFromDomainResponses]);
  }
}