﻿namespace Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public class GetOrgRepoCommitsOctokitDomainRequest
{
  public required string RepositoryName { get; set; }

  /// <summary>
  /// Where the repository statistics should be pulled from.
  /// This will be either a branch or a tag.
  /// </summary>
  public required string FromBranchOrTag { get; set; }

  /// <summary>
  /// Where the repository statistics should be pulled until.
  /// This will be either a branch or a tag.
  /// </summary>
  public required string ToBranchOrTag { get; set; }

  /// <summary>
  /// Whether merge commits should be filtered from the results
  /// </summary>
  public bool ExcludeMergeCommits { get; set; }
}