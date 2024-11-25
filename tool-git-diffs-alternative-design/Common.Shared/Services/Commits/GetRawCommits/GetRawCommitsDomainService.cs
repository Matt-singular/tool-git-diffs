namespace Common.Shared.Services.Commits.GetRawCommits;

using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Shared.Config;
using Common.Shared.Extensions.ThirdParty;
using Common.Shared.Models.Commits;
using Common.Shared.Models.ThirdParty;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetRawCommitsDomainService"/>
public class GetRawCommitsDomainService(IOptions<SecretSettings> secretSettings) : IGetRawCommitsDomainService
{
  private readonly OctokitApiClient apiClient = new(secretSettings.Value.GitHubAccessToken);

  /// <inheritdoc/>
  public async Task<List<RawCommitDetails>> ProcessAsync(long repositoryId, string fromReference, string toReference)
  {
    var getRawCommitsTask = this.GetRawCommitsFromOctokitAsync(repositoryId, fromReference, toReference);
    var getRawCommitsResponse = await getRawCommitsTask.ConfigureAwait(false);

    var domainResponse = MapRawCommitDetails(getRawCommitsResponse);
    return domainResponse;
  }

  private async Task<Octokit.CompareResult> GetRawCommitsFromOctokitAsync(long repositoryId, string fromReference, string toReference)
  {
    // Get the specified repository's commits from Octokit
    var getRepositoryCommitsOctokitTask = this.apiClient.Repository.Commit.Compare(repositoryId, @base: fromReference, head: toReference);
    var getRepositoryCommitsOctokitResponse = await getRepositoryCommitsOctokitTask.ConfigureAwait(false);

    return getRepositoryCommitsOctokitResponse;
  }

  private static List<RawCommitDetails> MapRawCommitDetails(Octokit.CompareResult getRawCommitsResponse)
  {
    // Takes the raw Octokit commit details and maps them to the domain model
    var rawCommitDetails = getRawCommitsResponse.Commits.Select(rawCommit => new RawCommitDetails
    {
      Hash = rawCommit.Sha,
      AuthorName = rawCommit.Author.Login,
      AuthorEmail = rawCommit.Commit.Author.Email,
      Message = rawCommit.Commit.Message,
      DateOfCommit = rawCommit.Commit.Author.Date.Date,
      IsMergeCommit = rawCommit.CheckIsMergeCommit()
    }).ToList();

    return rawCommitDetails;
  }
}