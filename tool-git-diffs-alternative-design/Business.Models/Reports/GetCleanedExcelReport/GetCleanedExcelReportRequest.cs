namespace Business.Models.Reports.GetCleanedExcelReport;

using Business.Models.Commits.GetCleanedCommits;

/// <summary>
/// Contains the data needed to generate an Excel report containing
/// the cleaned commits from one or more repositories
/// </summary>
public class GetCleanedExcelReportRequest(List<GetCleanedCommitsResponse.RepositoryCleanedCommitsDetail> repositoryCleanedCommits)
  : GetCleanedCommitsResponse(repositoryCleanedCommits)
{
}