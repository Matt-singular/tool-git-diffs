namespace Common.Shared.Tests.Config;

using Common.Shared.Config;
using Microsoft.Extensions.Options;
using NSubstitute;

/// <summary>
/// A mocked instance of <see cref="SecretSettings"/>
/// </summary>
public class MockedSecretSettings
{
  /// <summary>
  /// Creates a mocked instance of <see cref="IOptions{TOptions}"/> for <see cref="SecretSettings"/>
  /// </summary>
  /// <returns>The mocked instance</returns>
  public static IOptions<SecretSettings> CreateOptions()
  {
    var mockOptions = Substitute.For<IOptions<SecretSettings>>();

    mockOptions.Value.Returns(new SecretSettings
    {
      GitHubAccessToken = "Mock-access-token"
    });

    return mockOptions;
  }
}