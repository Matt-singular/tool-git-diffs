namespace ConfigExtraction.Tests.Models;

using System;
using ConfigExtraction.Models;

public class ConfigModelTests
{
  [Fact]
  public void BreakDiffRangeMutualExclusivityRule_ThrowInvalidOperation()
  {
    // Act
    var act = () =>
    {
      _ = new ConfigModel
      {
        DiffRange = new DiffRange
        {
          From = new DiffRangeValue
          {
            Branch = "Main",
            Tag = "12.0.4"
          }
        }
      };
    };

    // Assert
    act.Should()
      .Throw<InvalidOperationException>()
      .WithMessage(ConfigModel.Constants.Errors.BranchTagMutualExclusivityViolated);
  }

  [Fact]
  public void ValidDiffRangeMutualExclusivityConfigModel()
  {
    // Act
    var configModel = new ConfigModel
    {
      DiffRange = new DiffRange
      {
        From = new DiffRangeValue
        {
          Branch = "Main",
        }
      }
    };

    // Assert
    configModel.Should().NotBeNull();
  }
}