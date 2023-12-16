# 1 - Run the Unit tests and generate the coverlet code coverage
Clear-Host
Write-Host "Generating Coverlet Code Coverage for Project" -ForegroundColor Green
dotnet test --collect:"XPlat Code Coverage" "--results-directory:CoverageReport\CoverletRaw" # TODO: currently running twice to maintain output
$coverageResults = dotnet test --collect:"XPlat Code Coverage" "--results-directory:CoverageReport\CoverletRaw"
$combinedResults = $coverageResults -join ' '

# 2 - Extract the different coverage results for the test projects
$testPaths = @()
Write-Host
if ($combinedResults -match 'Attachments:\s*(.+)'){
  # Extract the attchment string and clean it up
  $attachmentString = $Matches.0
  $attachmentString = $attachmentString -replace 'Attachments:', ''
  $attachmentString = $attachmentString.Trim()
  $attachmentString = $attachmentString -replace '(\s+)', '#'
  #Write-Host "Attachment string is: $attachmentString"

  # Extract the full paths
  $fullPaths = $attachmentString -split "#"
  #Write-Host "The located paths are: $fullPaths"

  # Extract the relative paths
  #Write-Host "The Relative paths are:" -ForegroundColor Green
$relativePaths = New-Object System.Collections.ArrayList
  foreach ($fullPathString in $fullPaths)
  {
    # Clean each full path to a relative paths
    $relativePath = $fullPathString -replace '.*\\git-diff-tool\\', ''
    $_ = $relativePaths.Add($relativePath)
    #Write-Host "`t * $relativePath" -ForegroundColor Green
    
    # Concatenate the string
    if ([string]::IsNullOrEmpty($testPaths)) 
    {
      $testPaths = $relativePath
    } 
    else 
    {
      $testPaths = $testPaths + ";" + $relativePath
    }
  }
  #Write-Host "Relative Path string = $testPaths"
}

# 3 - Generate the CoverageReport
Write-Host "Generating Code Coverage Report" -ForegroundColor Green
dotnet "$env:USERPROFILE\.nuget\packages\reportgenerator\5.2.0\tools\net8.0\ReportGenerator.dll" "-reports:$testPaths" "-targetdir:CoverageReport\GeneratedReport"

# 4 - Create a shortcut for the generated CoverageReport
$targetPath = Join-Path $PSScriptRoot "CoverageReport\GeneratedReport\index.html"
$shell = New-Object -ComObject WScript.Shell
$shortcut = $shell.CreateShortcut("GeneratedReport.lnk")
$shortcut.TargetPath = $targetPath
$shortcut.Save()
Write-Host "A Shortcut has been saved to $targetPath`n" -ForegroundColor Green

# 5 - Keep the console open - allow the user to navigate to the generated report
Write-Host "Press Enter to Open the generated report, or Esc to Exit..."
$input = [System.Console]::ReadKey()

if ($input.Key -eq 'Enter')
{
  # Open the GeneratedReport shortcut
  $shortcutPath = Join-Path $PSScriptRoot "GeneratedReport.lnk"
  Write-Host "Opening $shortcutPath"
  Clear-Host
  Invoke-Item $shortcutPath
}

if ($input.Key -eq 'Esc')
{
  # Exit the script
  Clear-Host
  Invoke-Item $nop
  Clear-Host
}