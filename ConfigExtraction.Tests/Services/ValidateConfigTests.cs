namespace ConfigExtraction.Tests.Services;

using System.Text.Json;
using ConfigExtraction.Models;
using ConfigExtraction.Services;

public class ValidateConfigTests
{
  [Fact]
  public void CheckIfDefault_ForDefaultModel_ShouldThrowException()
  {
    // Arrange
    var config = new ConfigModel();
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var act = () => validateConfig.CheckIfDefault();

    // Assert
    act.Should()
      .Throw<JsonException>()
      .WithMessage(ValidateConfig.Constants.Errors.IsDefaultDueToFailedDeserialisation);
  }

  [Fact]
  public void CheckIfDefault_ForNullModel_ShouldThrowException()
  {
    // Arrange
    ConfigModel config = null!;
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var act = () => validateConfig.CheckIfDefault();

    // Assert
    act.Should()
      .Throw<JsonException>()
      .WithMessage(ValidateConfig.Constants.Errors.IsDefaultDueToFailedDeserialisation);
  }

  [Fact]
  public void CheckIfDefault_ForNonDefaultModel_ShouldReturnFalse()
  {
    // Arrange
    var config = new ConfigModel { DiffRange = new DiffRange() };
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var checkIfDefault = validateConfig.CheckIfDefault();

    // Assert
    checkIfDefault.Should().BeFalse();
  }

  public static IEnumerable<object[]> ValidDiffRanges()
  {
    yield return new object[] { Constants.ValidDiffRange.GlobalAndRepo };
    yield return new object[] { Constants.ValidDiffRange.GlobalOnly };
    yield return new object[] { Constants.ValidDiffRange.RepoOnly };
    yield return new object[] { Constants.ValidDiffRange.PartialGlobalAndRepo };
  }
  [Theory]
  [MemberData(nameof(ValidDiffRanges))]
  public void CheckDiffRangeSelection_ForValidSelections_ShouldReturnTrue(ConfigModel config)
  {
    // Arrange
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var checkIfDefault = validateConfig.CheckDiffRangeSelection();

    // Assert
    checkIfDefault.Should().BeTrue();
  }

  public static IEnumerable<object[]> InvalidDiffRanges()
  {
    yield return new object[] { Constants.InvalidDiffRange.NullConfig };
    yield return new object[] { Constants.InvalidDiffRange.NullGlobalDiffRange };
    yield return new object[] { Constants.InvalidDiffRange.NullRepository };
    yield return new object[] { Constants.InvalidDiffRange.NullRepoDiffRange };

    yield return new object[] { Constants.InvalidDiffRange.MissingDiffRangeValues };
    yield return new object[] { Constants.InvalidDiffRange.PartialGlobalDiffRange };
    yield return new object[] { Constants.InvalidDiffRange.PartialRepoDiffRange };
  }
  [Theory]
  [MemberData(nameof(InvalidDiffRanges))]
  public void CheckDiffRangeSelection_ForInvalidSelections_ShouldThrowException(ConfigModel config)
  {
    // Arrange
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var act = () => validateConfig.CheckDiffRangeSelection();

    // Assert
    act.Should()
      .Throw<InvalidDataException>()
      .WithMessage(ValidateConfig.Constants.Errors.InvalidDiffRangeSelection);
  }

  public static IEnumerable<object[]> ValidCommitReferences()
  {
    yield return new object[] { Constants.ValidCommitReferences.HasPattern };
    yield return new object[] { Constants.ValidCommitReferences.HasSubItems };
  }
  [Theory]
  [MemberData(nameof(ValidCommitReferences))]
  public void CheckCommitReferences_ForValidSelections_ShouldReturnTrue(ConfigModel config)
  {
    // Arrange
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var checkIfDefault = validateConfig.CheckCommitReferences();

    // Assert
    checkIfDefault.Should().BeTrue();
  }

  public static IEnumerable<object[]> InvalidCommitReferences()
  {
    yield return new object[] { Constants.InvalidCommitReferences.NullConfig };
    yield return new object[] { Constants.InvalidCommitReferences.NullReferences };
    yield return new object[] { Constants.InvalidCommitReferences.NullReferenceItem };

    yield return new object[] { Constants.InvalidCommitReferences.NullPattern };
    yield return new object[] { Constants.InvalidCommitReferences.EmptyPattern };
  }
  [Theory]
  [MemberData(nameof(InvalidCommitReferences))]
  public void CheckCommitReferences_ForInvalidSelections_ShouldThrowException(ConfigModel config)
  {
    // Arrange
    var validateConfig = Substitute.ForPartsOf<ValidateConfig>();
    validateConfig.Config = config;

    // Act
    var act = () => validateConfig.CheckCommitReferences();

    // Assert
    act.Should()
      .Throw<InvalidDataException>()
      .WithMessage(ValidateConfig.Constants.Errors.InvalidReferencePatterns);
  }

  public static class Constants
  {
    public static class ValidDiffRange
    {
      public static readonly ConfigModel GlobalAndRepo = new()
      {
        DiffRange = new DiffRange
        {
          From = new DiffRangeValue { Branch = "Dev" },
          To = new DiffRangeValue { Tag = "12.0.4" }
        },
        Repositories = [
          new Repository
          {
            DiffRange = new DiffRange
            {
              From = new DiffRangeValue { Branch = "Dev" }
            }
          }
        ]
      };
      public static readonly ConfigModel GlobalOnly = new()
      {
        DiffRange = new DiffRange
        {
          From = new DiffRangeValue { Branch = "Dev" },
          To = new DiffRangeValue { Tag = "12.0.4" }
        }
      };
      public static readonly ConfigModel RepoOnly = new()
      {
        Repositories = [
          new Repository
          {
            DiffRange = new DiffRange
            {
              From = new DiffRangeValue { Branch = "Dev" },
              To = new DiffRangeValue { Tag = "12.0.4" }
            }
          },
          new Repository
          {
            DiffRange = new DiffRange
            {
              From = new DiffRangeValue { Branch = "Main" },
              To = new DiffRangeValue { Tag = "12.0.4_special" }
            }
          }
        ]
      };
      public static readonly ConfigModel PartialGlobalAndRepo = new()
      {
        DiffRange = new DiffRange
        {
          From = new DiffRangeValue { Branch = "Dev" },
        },
        Repositories = [
          new Repository
          {
            DiffRange = new DiffRange
            {
              To = new DiffRangeValue { Branch = "12.0.4" }
            }
          }
        ]
      };
    }

    public static class InvalidDiffRange
    {
      public static readonly ConfigModel NullConfig = null!;
      public static readonly ConfigModel NullGlobalDiffRange = new()
      {
        DiffRange = null
      };
      public static readonly ConfigModel NullRepository = new()
      {
        Repositories = [null!]
      };
      public static readonly ConfigModel NullRepoDiffRange = new()
      {
        Repositories =
        [
          new Repository
          {
            DiffRange = null
          }
        ]
      };

      public static readonly ConfigModel MissingDiffRangeValues = new()
      {
        DiffRange = new DiffRange()
      };
      public static readonly ConfigModel PartialGlobalDiffRange = new()
      {
        DiffRange = new DiffRange
        {
          From = new DiffRangeValue { Branch = "Dev" }
        }
      };
      public static readonly ConfigModel PartialRepoDiffRange = new()
      {
        Repositories = [
          new Repository
          {
            DiffRange = new DiffRange
            {
              From = new DiffRangeValue { Branch = "Dev" },
              To = new DiffRangeValue { Tag = "12.0.4" },
            }
          },
          new Repository
          {
            DiffRange = new DiffRange
            {
              // No global diff range, so both the 'From' and the 'To' are required here
              From = new DiffRangeValue { Branch = "Main" }
            }
          }
        ]
      };
    }

    public static class ValidCommitReferences
    {
      public static readonly ConfigModel HasPattern = new()
      {
        References = [
          new Reference
          {
            Header = null,
            Pattern = "(FEATURE)-\\d+",
            SubItems = null
          }
        ]
      };
      public static readonly ConfigModel HasSubItems = new()
      {
        References = [
          new Reference
          {
            Header = null,
            Pattern = "(FEATURE)-\\d+",
            SubItems = ["(TASK)-\\d+"]
          }
        ]
      };
    }

    public static class InvalidCommitReferences
    {
      public static readonly ConfigModel NullConfig = null!;
      public static readonly ConfigModel NullReferences = new()
      {
        References = null!
      };
      public static readonly ConfigModel NullReferenceItem = new()
      {
        References = [null!]
      };

      public static readonly ConfigModel NullPattern = new()
      {
        References = [
          new Reference
          {
            Header = null,
            Pattern = null!,
            SubItems = null
          }
        ]
      };
      public static readonly ConfigModel EmptyPattern = new()
      {
        References = [
          new Reference
          {
            Header = null,
            Pattern = "",
            SubItems = null
          }
        ]
      };
    }
  }
}