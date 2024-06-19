# Change directory to the ApplicationConsole folder
Set-Location -Path '.\ApplicationConsole'

# Run dotnet publish for the ApplicationConsole.csproj project
dotnet publish ApplicationConsole.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true --output '..\PublishedFiles'

# Check
$response = Read-Host "Press Enter to exit..."