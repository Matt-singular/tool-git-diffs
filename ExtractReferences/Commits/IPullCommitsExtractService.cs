namespace ExtractReferences.Commits;

public interface IPullCommitsExtractService
{
  Task<GitHubCommitDetailsResponse> ProcessAsync(string repositoryName, string startRef, string endRef);
}