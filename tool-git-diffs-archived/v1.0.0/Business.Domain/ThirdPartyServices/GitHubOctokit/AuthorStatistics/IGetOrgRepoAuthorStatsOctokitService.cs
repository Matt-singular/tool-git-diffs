﻿namespace Business.Domain.ThirdPartyServices.GitHubOctokit.Statistics;

public interface IGetOrgRepoAuthorStatsOctokitService
{
  public Task<GetOrgRepoAuthorStatsOctokitDomainResponse> ProcessAsync(GetOrgRepoAuthorStatsOctokitDomainRequest request);
}