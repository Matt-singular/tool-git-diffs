namespace TestHelpers.Mocks;

using System;
using NSubstitute;

/// <summary>
/// General mock helpers
/// </summary>
public static class MockHelpers
{
  /// <summary>
  /// Creates a mocked instance of the specified service
  /// </summary>
  /// <typeparam name="TService">The interface of the service to mock</typeparam>
  /// <returns>The mocked instance of the service</returns>
  /// <exception cref="ArgumentException"></exception>
  public static TService MockService<TService>() where TService : class
  {
    // Ensure the TService provided is an interface as this allows proper mocking
    var isInterface = typeof(TService).IsInterface;
    if (!isInterface)
    {
      throw new ArgumentException("You must specify an interface when mocking a domain service.");
    }

    // Mock out the specified service
    var mockedService = Substitute.For<TService>();
    return mockedService;
  }
}