namespace DiffGeneration;

using Configuration.Settings;
using ExtractReferences.Commits;
using Microsoft.Extensions.Options;

public class OrchestrationDiffGenerationService(IOptions<CommitSettings> commitSettings, IOptions<SecretSettings> SecretSettings,
  IPullCommitsExtractService pullCommitsExtractService) : IOrchestrationDiffGenerationService
{
  private readonly SecretSettings SecretSettings = SecretSettings.Value;
  private readonly CommitSettings CommitSettings = commitSettings.Value;
  private readonly IPullCommitsExtractService PullCommitsExtractService = pullCommitsExtractService;

  public Task ProcessAsync(string build, string from, string to)
  {
    var rawDiffs = PullRawDiffs(from, to);
    return null;
  }

  public async Task<List<GitHubCommitDetailsResponse>> PullRawDiffs(string from, string to)
  {
    var repositories = this.SecretSettings.GitHubRepositories;

    var pullCommitTasks = repositories
      .Select(repoName => this.PullCommitsExtractService.ProcessAsync(repoName, from, to))
      .ToList();

    var pullCommitResponses = await Task.WhenAll(pullCommitTasks).ConfigureAwait(false);

    return pullCommitResponses.ToList();
  }

  public Task CleanDiffs()
  {
    throw new NotImplementedException();
  }

  public Task GenerateTextFile(string build)
  {
    throw new NotImplementedException();
  }
}

public interface IOrchestrationDiffGenerationService
{
  Task ProcessAsync(string build, string from, string to);
  Task<List<GitHubCommitDetailsResponse>> PullRawDiffs(string from, string to);
  Task CleanDiffs();
  Task GenerateTextFile(string build);
}