namespace Common.Shared.Services.Commits.GetCleanedCommits;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Common.Shared.Config;
using Common.Shared.Extensions.Config;
using Common.Shared.Models.Commits;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IGetCleanedCommitsDomainService"/>
public class GetCleanedCommitsDomainService(IOptions<CommitSettings> commitSettings) : IGetCleanedCommitsDomainService
{
  private List<Regex> jiraReferencePatterns = null!;

  /// <inheritdoc />
  public List<CleanedCommitDetails> Process(List<RawCommitDetails> rawCommits)
  {
    ArgumentNullException.ThrowIfNull(rawCommits, nameof(rawCommits));
    ArgumentNullException.ThrowIfNull(commitSettings.Value, nameof(commitSettings));

    this.jiraReferencePatterns = commitSettings.Value.CreateRegexes();
    var cleanedCommits = rawCommits.Select(MapCleanedCommitDetails).ToList();

    return cleanedCommits;
  }

  private CleanedCommitDetails MapCleanedCommitDetails(RawCommitDetails rawCommit)
  {
    var cleanedCommitDetail = new CleanedCommitDetails(rawCommit);
    var priority = 0;

    foreach (var regex in this.jiraReferencePatterns)
    {
      var references = regex.Matches(rawCommit.Message).Select(match => new CommitReference
      {
        JiraReference = match.Value,
        Priority = priority
      }).ToList();

      cleanedCommitDetail.JiraReferences.AddRange(references);

      priority++;
    }

    return cleanedCommitDetail;
  }
}