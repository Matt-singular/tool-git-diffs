﻿namespace Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public interface IGetRepositoryStatisticsOctokitService
{
  public Task<GetRepositoryStatisticsOctokitResponse> ProcessAsync(GetRepositoryStatisticsOctokitRequest request);
}