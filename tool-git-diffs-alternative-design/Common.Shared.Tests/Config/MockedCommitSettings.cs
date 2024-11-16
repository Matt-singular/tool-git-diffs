namespace Common.Shared.Tests.Config;

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
  public static IOptions<CommitSettings> CreateOptions()
  {
    var mockOptions = Substitute.For<IOptions<CommitSettings>>();
    return mockOptions;
  }
}