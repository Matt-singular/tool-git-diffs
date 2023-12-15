namespace ConfigExtraction.Tests;

using ConfigExtraction.Services;
using ConfigExtraction.Models;

public class OrchestrationTests
{
  [Fact]
  public void IntegrationTest()
  {
    // Arrange
    var config = new ConfigModel
    {
      DiffRange = new DiffRange
      {
        From = new DiffRangeValue
        {
          Branch = "Dev"
        },
        To = new DiffRangeValue
        {
          Tag = "12.0.4"
        }
      },
      References =
        [
          new Reference
          {
            Pattern = "(FEAT)-\\d+"
          }
        ]
    };
    var mockedReadConfigService = Substitute.For<IReadConfig>();
    mockedReadConfigService.Process().Returns(config);
    var mockedValidateConfig = Substitute.For<IValidateConfig>();
    var configExtractionOrchestration = Substitute.ForPartsOf<Orchestration>(mockedReadConfigService, mockedValidateConfig);

    // Act
    configExtractionOrchestration.Process();

    // Assert
    configExtractionOrchestration.Received(1).Process();
  }
}