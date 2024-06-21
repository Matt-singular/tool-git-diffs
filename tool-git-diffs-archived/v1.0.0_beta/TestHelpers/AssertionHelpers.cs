namespace TestHelpers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for asserting conditions in unit tests.
/// </summary>
public static class AssertionHelpers
{
  /// <summary>
  /// Asserts that the specified service is registered in the service collection with the expected lifetime.
  /// </summary>
  /// <typeparam name="TServiceInterface">The type of the service interface.</typeparam>
  /// <typeparam name="TServiceConcrete">The type of the service implementation.</typeparam>
  /// <param name="services">The service collection.</param>
  /// <param name="expectedLifetime">The expected service lifetime.</param>
  /// <param name="serviceName">The name of the service.</param>
  /// <exception cref="ArgumentException">Thrown when the service is not registered with the expected lifetime.</exception>
  public static void AssertServiceDI<TServiceInterface, TServiceConcrete>(IServiceCollection services, ServiceLifetime expectedLifetime, string serviceName)
  {
    try
    {
      services.Should().ContainSingle(descriptor => TestingHelpers.ValidDependencyType<TServiceInterface, TServiceConcrete>(descriptor, expectedLifetime));
    }
    catch (Exception ex)
    {
      throw new ArgumentException(serviceName, ex.Message);
    }
  }
}