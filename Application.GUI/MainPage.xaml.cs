namespace Application.GUI;

using Business.Domain.Services.RepositoryStatistics;
using Business.Domain.Services.RepositoryStatistics.GetOrgRepoCleanedCommits;
using Business.Infrastructure.Services.RepositoryStatisticsl;

public partial class MainPage : ContentPage
{
  private string FromReference { get => FromReferenceEntryElement.Text; }
  private string ToReference { get => ToReferenceEntryElement.Text; }
  private readonly IGetOrgRepoCleanedCommitsDomainService getOrgRepoCleanedCommitsDomainService;

  public MainPage(IGetOrgRepoCleanedCommitsDomainService getOrgRepoCleanedCommitsDomainService)
  {
    this.getOrgRepoCleanedCommitsDomainService = getOrgRepoCleanedCommitsDomainService;

    InitializeComponent();
  }

  private void OnGenerateClicked(object sender, EventArgs e)
  {
    var request = new GetOrgRepoCleanedCommitsDomainRequest
    {
      // Update placeholder data
      Repositories = [new IGetRepoCommitsDomainRequest.Repository { RepositoryName = "testing" }],
      FromBranchOrTag = FromReference,
      ToBranchOrTag = ToReference
    };

    var result = getOrgRepoCleanedCommitsDomainService.GetCleanedCommits(request).GetAwaiter().GetResult();
  }
}