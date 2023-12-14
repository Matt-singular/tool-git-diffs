namespace ConfigExtraction.Tests.Services;

using System.Text.Json;
using ConfigExtraction.Models;
using ConfigExtraction.Services;

/// <summary>
/// The unit tests for the ConfigExtraction.Services.ReadConfig
/// </summary>
public class ReadConfigTests
{
  [Fact]
  public void SuccessfulDeserialisation_NotNull()
  {
    // Arrange - FileServices (mocked) -- TODO: extract to a set of mocked helper functions?
    var mockedFilePath = "path/to/file.txt";
    var mockedFileContents = @"{
      ""diffRange"": {
        ""from"": {
          ""branch"": null,
          ""tag"": ""12.0.4""
        },
        ""to"": {
          ""branch"": ""main"",
          ""tag"": null
        }
      }
    }";
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(true);
    mockedFileServices.ReadText(mockedFilePath).Returns(mockedFileContents);

    // Arrange - ReadConfig service to test
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var configResults = readConfig.Process();

    // Assert
    configResults.Should().NotBeNull();
  }

  [Fact]
  public void SuccessfulDeserialisation_ShouldEqualModel()
  {
    // Arrange - FileServices (mocked) -- TODO: extract to a set of mocked helper functions?
    var mockedFilePath = "path/to/file.txt";
    var mockedFileContents = @"{
      ""diffRange"": {
        ""from"": {
          ""branch"": null,
          ""tag"": ""12.0.4""
        },
        ""to"": {
          ""branch"": ""main"",
          ""tag"": null
        }
      },
      ""commitOptions"": {
        ""captureCommitsWithoutReferences"": true,
        ""groupReferencesByHeader"": false
      },
      ""references"": [{
          ""header"": ""Features"",
          ""pattern"": ""(FEATURE)-\\d+"",
          ""subItems"": [ ""(TASK)-\\d+"" ]
        }],
      ""repositories"": [{
          ""name"": ""Api"",
          ""path"": ""C:\\Clients\\Project\\Automation\\Diffs\\Repos\\Api-Repo"",
          ""diffRange"": {
            ""from"": {
              ""branch"": null,
              ""tag"": null
            },
            ""to"": {
              ""branch"": ""dev"",
              ""tag"": null
            }
          }
      }]
    }";
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(true);
    mockedFileServices.ReadText(mockedFilePath).Returns(mockedFileContents);

    // Arrange - ReadConfig service to test
    var readConfigExpected = new ConfigModel
    {
      DiffRange = new DiffRange
      {
        From = new DiffRangeValue { Tag = "12.0.4" },
        To = new DiffRangeValue { Branch = "main" },
      },
      CommitOptions = new Commitoptions
      {
        CaptureCommitsWithoutReferences = true,
        GroupReferencesByHeader = false
      },
      References =
      [
        new Reference
        {
          Header = "Features",
          Pattern = "(FEATURE)-\\d+",
          SubItems = ["(TASK)-\\d+"]
        }
      ],
      Repositories =
      [
        new Repository
        {
          Name = "Api",
          Path = "C:\\Clients\\Project\\Automation\\Diffs\\Repos\\Api-Repo",
          DiffRange = new DiffRange
          {
            From = new DiffRangeValue(),
            To = new DiffRangeValue { Branch = "dev" },
          }
        }
      ]
    };
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var configResults = readConfig.Process();

    // Assert
    configResults.Should().NotBeNull();
    configResults.Should().BeEquivalentTo(readConfigExpected);
  }

  [InlineData("{}")] // Empty Json, will be deserialised to a default ConfigModel
  [InlineData("null")] // Null Json, will be deserailised to null
  [Theory]
  public void FailedDeserialisation_ThrowsJsonError(string mockedFileContents)
  {
    // Arrange - Mocked
    var mockedFilePath = "path/to/file.txt";
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(true);
    mockedFileServices.ReadText(mockedFilePath).Returns(mockedFileContents);

    // Arrange - ReadConfig service to test
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var act = () => readConfig.Process();

    // Assert
    act.Should()
      .Throw<JsonException>()
      .WithMessage(ReadConfig.Constants.Errors.FailedDeserialisation);
  }

  [Fact]
  public void MissingFile_ThrowsFileError()
  {
    // Arrange - Mocked
    var mockedFilePath = "path/to/file.txt";
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(false);

    // Arrange - ReadConfig service to test
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var act = () => readConfig.Process();

    // Assert
    act.Should()
      .Throw<FileNotFoundException>()
      .WithMessage(ReadConfig.Constants.Errors.FileNotFound);
  }

  [Fact]
  public void NotValidJson_ThrowsUnhandledJsonError()
  {
    // Arrange - Mocked
    var mockedFilePath = "path/to/file.txt";
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(true);

    // Arrange - ReadConfig service to test
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var act = () => readConfig.Process();

    // Assert
    act.Should().Throw<JsonException>();
  }

  [Fact]
  public void FailedDeserialisation_BecauseMissingComma_ThrowJsonError()
  {
    // Arrange - Mocked
    var mockedFilePath = "path/to/file.txt";
    var mockedFileContents = @"{
      ""diffRange"": {
        ""from"": {
          ""branch"": null,
          ""tag"": ""12.0.4""
        }
        ""to"": {
          ""branch"": ""main"",
          ""tag"": null
        }
      }
    }"; // This will fail deserialisation as there's a missing comma
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(true);
    mockedFileServices.ReadText(mockedFilePath).Returns(mockedFileContents);

    // Arrange - ReadConfig service to test
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var act = () => readConfig.Process();

    // Assert
    act.Should().Throw<JsonException>();
  }

  [Fact]
  public void BreakConfigModelDiffRangeMutualExclusivityRule_ThrowInvalidOperation()
  {
    // Arrange - Mocked
    var mockedFilePath = "path/to/file.txt";
    var mockedFileContents = @"{
      ""diffRange"": {
        ""from"": {
          ""branch"": ""main"",
          ""tag"": ""12.0.4""
        }
      }
    }"; // Violated the rule that states you should set EITHER the branch OR the tag (NOT BOTH)
    var mockedFileServices = Substitute.For<IFileServices>();
    mockedFileServices.GetFullPath().Returns(mockedFilePath);
    mockedFileServices.Exists(mockedFilePath).Returns(true);
    mockedFileServices.ReadText(mockedFilePath).Returns(mockedFileContents);

    // Arrange - ReadConfig service to test
    var readConfig = new ReadConfig(mockedFileServices);

    // Act
    var act = () => readConfig.Process();

    // Assert
    act.Should()
      .Throw<InvalidOperationException>()
      .WithMessage(ConfigModel.Constants.Errors.BranchTagMutualExclusivityViolated);
  }
}