namespace ReferenceExtraction.Tests.Services;

using System.Text.RegularExpressions;
using ConfigExtraction.Models;
using ReferenceExtraction.Services;

public class GenerateRegexesTests
{
  [Fact]
  public void GenerateRegex_ShouldReturnRegexPatterns()
  {
    // Arrange
    var config = new ConfigModel
    {
      References = [
        new Reference
        {
          Header = "Features",
          Pattern = "(FEATURES)-\\d+",
          SubItems = null
        }
      ]
    };
    var expectedRegexPattern = new Regex("(FEATURES)-\\d+");
    var generateRegexes = Substitute.ForPartsOf<GenerateRegexes>();

    // Act
    var regexPatterns = generateRegexes.Process(config);
    var regexPattern = regexPatterns.First().Pattern;

    // Assert
    regexPatterns.Should().NotBeNullOrEmpty();
    regexPattern.Should().BeEquivalentTo(expectedRegexPattern);
  }

  [Fact]
  public void GenerateRegex_WithSubItems_ShouldReturnRegexPatterns()
  {
    // Arrange
    var config = new ConfigModel
    {
      References = [
        new Reference
        {
          Header = "Features",
          Pattern = "(FEATURES)-\\d+",
          SubItems = ["(TASK)-\\d+", "(ACTION)-\\d+"]
        }
      ]
    };
    var expectedRegexPattern = new Regex("(FEATURES)-\\d+");
    var expectedRegexSubItems = new List<Regex> { new Regex("(TASK)-\\d+"), new Regex("(ACTION)-\\d+") };
    var generateRegexes = Substitute.ForPartsOf<GenerateRegexes>();

    // Act
    var regexPatterns = generateRegexes.Process(config);
    var reference = regexPatterns.First();

    // Assert
    regexPatterns.Should().NotBeNullOrEmpty();
    reference.Pattern.Should().BeEquivalentTo(expectedRegexPattern);
    reference.SubPatterns.Should().BeEquivalentTo(expectedRegexSubItems);
  }

  [Fact]
  public void GenerateRegexBadData_WithEmptyReferences_ShouldBeEmpty()
  {
    // Arrange
    var config = new ConfigModel
    {
      References = [] // the config validation would fail so this isn't really a scenario we need to worry about, but just in case we have a test case for it :)
    };
    var generateRegexes = Substitute.ForPartsOf<GenerateRegexes>();

    // Act
   var regexPatterns = generateRegexes.Process(config);

    // Assert
    regexPatterns.Should().BeEmpty();
  }

  [Fact]
  public void GenerateRegexBadData_WithNullConfig_ShouldThrowNullReference()
  {
    // Arrange
    ConfigModel config = null!;
    var generateRegexes = Substitute.ForPartsOf<GenerateRegexes>();

    // Act
    var act = () => generateRegexes.Process(config);

    // Assert
    act.Should().Throw<NullReferenceException>(); ;
  }

  [Fact]
  public void GenerateRegexBadData_WithNullReferenceValues_ShouldThrowArgumentNull()
  {
    // Arrange
    var config = new ConfigModel
    {
      References = [
        new Reference
        {
          Header = null,
          Pattern = null!,
          SubItems = [null!]
        }
      ]
    };
    var generateRegexes = Substitute.ForPartsOf<GenerateRegexes>();

    // Act
    var act = () => generateRegexes.Process(config);

    // Assert
    act.Should().Throw<ArgumentNullException>();
  }

  [Fact]
  public void GenerateRegexBadData_WithReferenceArrayNullValue_ShouldThrowNullReference()
  {
    // Arrange
    var config = new ConfigModel
    {
      References = [null!]
    };
    var generateRegexes = Substitute.ForPartsOf<GenerateRegexes>();

    // Act
    var act = () => generateRegexes.Process(config);

    // Assert
    act.Should().Throw<NullReferenceException>();
  }
}