namespace ConfigExtraction.Tests.Services;

using ConfigExtraction.Services;
using NSubstitute;
using NSubstitute.Extensions;

/// <summary>
/// The unit tests for the ConfigExtraction.Services.FileServices logic
/// </summary>
public class FileServicesTests()
{
  [Fact]
  public void GetFilePathSuccess_ShouldReturnPath()
  {
    // Arrange
    var defaultFileName = "defaultConfig.json";
    var fileServices = Substitute.ForPartsOf<FileServices>();
    fileServices.Configure().SetDefaultFileName().Returns(defaultFileName); // NOTE it is vitally important that SetDefaultFDileName is a virtual method for this to work

    // Act
    var result = fileServices.GetFullPath();

    // Assert 
    result.Should().ContainAny($"{Constants.filePathBaseDebugMode}{defaultFileName}", $"{Constants.filePathBaseReleaseMode}{defaultFileName}");
  }

  [Fact]
  public void GetFilePathSuccess_ForFileName_DontCallDefaultFileName_ShouldReturnPath()
  {
    // Arrange
    var fileName = "testConfig.json";
    var fileServices = Substitute.ForPartsOf<FileServices>();

    // Act
    var result = fileServices.GetFullPath(fileName);

    // Assert 
    result.Should().ContainAny($"{Constants.filePathBaseDebugMode}{fileName}", $"{Constants.filePathBaseReleaseMode}{fileName}");
    fileServices.DidNotReceive().SetDefaultFileName();
  }

  [Fact]
  public void SetDefaultFileName_DebugAndReleaseMode()
  {
    // Arrange
    var fileServices = Substitute.ForPartsOf<FileServices>();

    // Act
    var result = fileServices.SetDefaultFileName();

    // Assert 
    result.Should().BeOneOf(FileServices.Constants.defaultFileNameDebugMode, FileServices.Constants.defaultFileNameReleaseMode);
  }

  [Theory]
  [InlineData("Files/configExample.json", true)] // Existing File
  [InlineData("fileDoesntExist.json", false)] // File doesn't exist
  public void CheckFileExistsScenarios(string fileName, bool expectedResult)
  {
    // Arrange
    var fileServices = Substitute.ForPartsOf<FileServices>();
    var path = fileServices.GetFullPath(fileName);

    // Act
    var result = fileServices.Exists(path);

    // Assert 
    result.Should().Be(expectedResult);
  }

  [Fact]
  public void ReadFileSuccess_ShouldReturnFileContent()
  {
    // Arrange
    var fileServices = Substitute.ForPartsOf<FileServices>();
    var path = fileServices.GetFullPath("Files/configExample.json");

    // Act
    var result = fileServices.ReadText(path);

    // Assert
    result.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public void ReadFileFailure_FileNotFound()
  {
    // Arrange
    var fileServices = Substitute.ForPartsOf<FileServices>();
    var path = fileServices.GetFullPath("Files/fake.json");

    // Act
    var result = fileServices.ReadText(path);

    // Assert
    result.Should().BeNullOrEmpty();
  }

  public static class Constants
  {
    public const string filePathBaseDebugMode = "git-diff-tool\\ConfigExtraction.Tests\\bin\\Debug\\net8.0\\";
    public const string filePathBaseReleaseMode = "git-diff-tool\\ConfigExtraction.Tests\\bin\\Release\\net8.0\\";
  }
}