﻿namespace Business.Infrastructure.ThirdPartyServices.GitHubOctokit.Authorisation;

using Business.Domain.ThirdPartyServices.GitHubOctokit.Authorisation;
using Common.Shared.Config;
using Microsoft.Extensions.Options;

/// <summary>
/// Fetches the GitHub access token from the secret settings and creates an authorised Octokit API client.
/// </summary>
/// <param name="secretSettings">Contains the configured GitHub access token which determines the capability of the Octokit API</param>
public class GetAuthorisedApiClientOctokitService(IOptions<SecretSettings> secretSettings) : IGetAuthorisedApiClientOctokitService
{
  private readonly SecretSettings secretSettings = secretSettings.Value;

  public GetAuthorisedApiClientOctokitResponse CreateClient()
  {
    var accessToken = this.secretSettings.GitHubAccessToken;
    var octokitApiClient = new GetAuthorisedApiClientOctokitResponse(accessToken);

    return octokitApiClient;
  }
}