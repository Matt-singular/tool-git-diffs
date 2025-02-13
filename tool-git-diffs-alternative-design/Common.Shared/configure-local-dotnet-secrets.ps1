# Disclaimers
Write-Host "Configuring dotnet secrets based on provided input"
Write-Host ""

# User Input
$AccessToken = Read-Host "Enter the GitHub access token"
$OrganisationName = Read-Host "Enter the GitHub organisation name"
$RepositoriesString = Read-Host "Enter the comma-separated list of GitHub repository names"
$Repositories = $RepositoriesString -split ','
Write-Host ""

# Configure the GitHub Secrets
dotnet user-secrets set "Secrets:GitHubAccessToken" $AccessToken.Trim()
dotnet user-secrets set "Secrets:GitHubOrganisation" $OrganisationName.Trim()
for ($index = 0; $index -lt $Repositories.Length; $index++) {
  $repository = $Repositories[$index]
  dotnet user-secrets set "Secrets:GitHubRepositories:$index" $repository.Trim()
}
Write-Host ""

# Success Message
Write-Host "Dotnet secrets configured successfully" -ForegroundColor Green