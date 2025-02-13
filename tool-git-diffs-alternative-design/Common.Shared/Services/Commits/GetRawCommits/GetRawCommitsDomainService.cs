namespace Common.Shared.Services.Commits.GetRawCommits;

using System;
using System.Collections.Generic;
using System.Text;
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
    var getRawCommitsTask = this.GetRawCommitsFromOctokitAsync(repositoryId, null, null, fromReference, toReference);
    var getRawCommitsResponse = await getRawCommitsTask.ConfigureAwait(false);

    var domainResponse = MapRawCommitDetails(getRawCommitsResponse);
    return domainResponse;
  }

  /// <inheritdoc/>
  public async Task<List<RawCommitDetails>> ProcessAsync(string repositoryName, string repositoryOwner, string fromReference, string toReference)
  {
    var getRawCommitsTask = this.GetRawCommitsFromOctokitAsync(null, repositoryName, repositoryOwner, fromReference, toReference);
    var getRawCommitsResponse = await getRawCommitsTask.ConfigureAwait(false);

    var domainResponse = MapRawCommitDetails(getRawCommitsResponse);
    return domainResponse;
  }

  private async Task<Octokit.CompareResult> GetRawCommitsFromOctokitAsync(long? repositoryId, string? repositoryName, string? repositoryOwner,
    string fromReference, string toReference)
  {
    // Validate input
    GuardInputValidation(repositoryId, repositoryName, repositoryOwner, fromReference, toReference);

    // Get the specified repository's commits from Octokit using either the repository id OR the repository name & owner
    var getRepositoryCommitsOctokitTask = repositoryId is not null
      ? this.apiClient.Repository.Commit.Compare(repositoryId.Value, @base: fromReference, head: toReference)
      : this.apiClient.Repository.Commit.Compare(repositoryOwner, repositoryName, @base: fromReference, head: toReference);

    var getRepositoryCommitsOctokitResponse = await getRepositoryCommitsOctokitTask.ConfigureAwait(false);

    return getRepositoryCommitsOctokitResponse;
  }

  private static void GuardInputValidation(long? repositoryId, string? repositoryName, string? repositoryOwner, string fromReference, string toReference)
  {
    // Various Input Validations
    var validReferences = !string.IsNullOrWhiteSpace(fromReference) && !string.IsNullOrWhiteSpace(toReference);
    var validRepositoryId = repositoryId is not null;
    var validRepositoryName = !string.IsNullOrWhiteSpace(repositoryName) && !string.IsNullOrWhiteSpace(repositoryOwner);

    // Handle Input Errors
    var messages = new StringBuilder();

    if (validReferences is false)
    {
      var invalidReferencesMessage = "The Git references (from and to) are invalid or missing";
      messages.AppendLine(invalidReferencesMessage);
    }

    if (validRepositoryId is false && validRepositoryName is false)
    {
      if (validRepositoryId is false)
      {
        var invalidRepositoryIdMessage = "The Git repository id is missing";
        messages.AppendLine(invalidRepositoryIdMessage);
      }

      if (validRepositoryName is false)
      {
        var invalidRepositoryNameMessage = "The Git repository name and/or owner is missing";
        messages.AppendLine(invalidRepositoryNameMessage);
      }
    }

    // Compile the error message
    var compiledMessage = messages.ToString();
    if (string.IsNullOrEmpty(compiledMessage))
    {
      return;
    }

    throw new ArgumentException(compiledMessage);
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