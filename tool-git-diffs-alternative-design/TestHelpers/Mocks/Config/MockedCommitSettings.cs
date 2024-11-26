namespace TestHelpers.Mocks.Config;

using Common.Shared.Config;
using Microsoft.Extensions.Options;
using NSubstitute;

/// <summary>
/// A mocked instance of <see cref="CommitSettings"/>
/// </summary>
public static class MockedCommitSettings
{
  /// <summary>
  /// Creates a mocked instance of <see cref="IOptions{TOptions}"/> for <see cref="CommitSettings"/>
  /// </summary>
  /// <returns>The mocked instance</returns>
  public static IOptions<CommitSettings> CreateEmptyOptions()
  {
    var mockOptions = Substitute.For<IOptions<CommitSettings>>();

    return mockOptions;
  }

  /// <summary>
  /// Creates a mocked instance of <see cref="IOptions{TOptions}"/> for <see cref="CommitSettings"/>
  /// </summary>
  /// <returns>The mocked instance</returns>
  public static IOptions<CommitSettings> CreateOptionsScenarioA()
  {
    var mockedCommitOptions = CreateEmptyOptions();

    mockedCommitOptions.Value.Returns(new CommitSettings
    {
      Patterns =
      [
        new("(DEFECT)-?\\d{3,4}", 2, "Defects"),
        new("(FEAT)-?\\d{3,4}", 1, "Features"),
        new("(DEV)-?\\d{3,4}", 2, "Features")
      ],
      SortOrder = ["Features", "Defects"]
    });

    return mockedCommitOptions;
  }
}