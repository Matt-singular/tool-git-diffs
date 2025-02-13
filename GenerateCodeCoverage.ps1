### This script will run dotnet tests and then generate a code coverage report from the results
### If you receive a digital signing error then run the follwoing command: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process

# 1. Clear out the contents of the TestResults folder
function Clear-TestResultsFolder 
{
    param ([string]$path)
    if (Test-Path -Path $path) {
        Get-ChildItem -Path "$path/TestResults" | Remove-Item -Recurse -Force
    }
}

Clear-TestResultsFolder -path "Business.Domain.Tests"


# 2. Generate the code coverage using Coverlet
dotnet test --collect:"XPlat Code Coverage"

# 3. Generate a CoverageReport
$reportPaths = @(
    "Business.Domain.Tests/TestResults/*/coverage.cobertura.xml"
)

$reportPathsString = [string]::Join(";", $reportPaths)

reportgenerator -reports:$reportPathsString -targetdir:Common.Shared/CodeCoverage