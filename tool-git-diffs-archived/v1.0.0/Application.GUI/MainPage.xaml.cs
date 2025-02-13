namespace Application.GUI;

using Business.Domain.Services.Excel;
using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public partial class MainPage : ContentPage
{
  private string FromReference { get => FromReferenceEntryElement.Text; }
  private string ToReference { get => ToReferenceEntryElement.Text; }

  private readonly IGetOrgRepoCommitsOctokitService getOrgRepoCommitsOctokitService;
  private readonly ICreateExcelSheetDomainService createExcelSheetDomainService;

  public MainPage(IGetOrgRepoCommitsOctokitService getOrgRepoCommitsOctokitService, ICreateExcelSheetDomainService createExcelSheetDomainService)
  {
    this.getOrgRepoCommitsOctokitService = getOrgRepoCommitsOctokitService;
    this.createExcelSheetDomainService = createExcelSheetDomainService;

    InitializeComponent();
  }

  private void OnGenerateClicked(object sender, EventArgs e)
  {
    // POC TEST 1
    var domainRequest = new GetOrgRepoCommitsOctokitDomainRequest
    {
      RepositoryName = "placeholder",
      FromBranchOrTag = FromReference,
      ToBranchOrTag = ToReference,
      ExcludeMergeCommits = false
    };

    //var domainResponse = getOrgRepoCommitsOctokitService.ProcessAsync(domainRequest).GetAwaiter().GetResult();

    // POC TEST 2
    createExcelSheetDomainService.Process();
  }

  private async Task ShowSimpleAlert() => await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Alert", "message", "Ok");
}