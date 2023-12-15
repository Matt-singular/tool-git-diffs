# Run the Unit tests and generate the coverlet code coverage
Write-Host "Generating Coverlet Code Coverage for Project" -ForegroundColor Green
dotnet test --collect:"XPlat Code Coverage" "--results-directory:CoverageReport\CoverletRaw" # TODO: currenlty running twice to maintain output
$coverageResults = dotnet test --collect:"XPlat Code Coverage" "--results-directory:CoverageReport\CoverletRaw"
$combinedResults = $coverageResults -join ' '

# Extract the Relative Path and Navigate to the Coverlet CoverageReport directory
if ($combinedResults -match 'Attachments:\s*(.+)') 
{
  # Full Path
  $fullPathString = $Matches.0
  $fullPathString = $fullPathString.Trim()
  #Write-Output "Extracted path string = $fullPathString`n"

  # Relative Path
  $relativePath = $fullPathString -replace '.*\\git-diff-tool\\', ''
  $relativePath = $relativePath -replace 'coverage.cobertura.xml', ''
  #$relativePath = ".\$relativePath"
  Write-Host "`nRelative path = $relativePath" -ForegroundColor Green

  # Coverlet CoverageReport Directory
  Set-Location $relativePath
}

# Generate the CoverageReport
$reportPath = "..\..\GeneratedReport"
dotnet "$env:USERPROFILE\.nuget\packages\reportgenerator\5.2.0\tools\net8.0\ReportGenerator.dll" -reports:coverage.cobertura.xml "-targetdir:$reportPath"

# Create a shortcut for the generated CoverageReport
$targetPath = Join-Path $PSScriptRoot "CoverageReport\GeneratedReport\index.html"
$shell = New-Object -ComObject WScript.Shell
$shortcut = $shell.CreateShortcut("GeneratedReport.lnk")
$shortcut.TargetPath = $targetPath
$shortcut.Save()
Write-Host "A Shortcut has been saved to $targetPath`n" -ForegroundColor Green

# Keep the console opening to help with debugging
Read-Host "Press Enter to Exit..."