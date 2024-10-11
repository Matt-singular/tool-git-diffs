namespace Application.GUI;

using Business.Domain.ThirdPartyServices.GitHubOctokit.RepositoryStatistics;

public partial class MainPage : ContentPage
{
  private string FromReference { get => FromReferenceEntryElement.Text; }
  private string ToReference { get => ToReferenceEntryElement.Text; }
  private readonly IGetOrgRepoCommitsOctokitService getOrgRepoCommitsOctokitService;

  public MainPage(IGetOrgRepoCommitsOctokitService getOrgRepoCommitsOctokitService)
  {
    this.getOrgRepoCommitsOctokitService = getOrgRepoCommitsOctokitService;

    InitializeComponent();
  }

  private void OnGenerateClicked(object sender, EventArgs e)
  {
    var domainRequest = new GetOrgRepoCommitsOctokitDomainRequest
    {
      RepositoryName = "placeholder",
      FromBranchOrTag = FromReference,
      ToBranchOrTag = ToReference,
      ExcludeMergeCommits = false
    };

    var domainResponse = getOrgRepoCommitsOctokitService.ProcessAsync(domainRequest).GetAwaiter().GetResult();
  }
}