﻿namespace Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public interface IGetOrgRepoCommitsOctokitService
{
  public Task<GetOrgRepoCommitsOctokitDomainResponse> ProcessAsync(GetOrgRepoCommitsOctokitDomainRequest request);
}