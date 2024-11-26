namespace Common.Shared.Services.Commits.GetCleanedCommits;

using System;
using System.Collections.Generic;
using Common.Shared.Models.Commits;

/// <inheritdoc cref="IGetCleanedCommitsDomainService"/>
public class GetCleanedCommitsDomainService : IGetCleanedCommitsDomainService
{
  /// <inheritdoc />
  public List<CleanedCommitDetails> Process(List<RawCommitDetails> rawCommits)
  {
    ArgumentNullException.ThrowIfNull(rawCommits, nameof(rawCommits));

    var cleanedCommits = rawCommits.Select(MapCleanedCommitDetails).ToList();

    return cleanedCommits;
  }

  private CleanedCommitDetails MapCleanedCommitDetails(RawCommitDetails rawCommit)
  {
    throw new NotImplementedException();
  }
}