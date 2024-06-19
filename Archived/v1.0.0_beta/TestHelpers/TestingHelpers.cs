namespace TestHelpers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for testing.
/// </summary>
public static class TestingHelpers
{
  /// <summary>
  /// Validates the Dependency Injection type.
  /// </summary>
  /// <typeparam name="TServiceInterface">The type of the service interface.</typeparam>
  /// <typeparam name="TServiceConcrete">The type of the service implementation.</typeparam>
  /// <param name="serviceDescriptor">The service descriptor to validate.</param>
  /// <param name="serviceLifetime">The expected service lifetime.</param>
  /// <returns>True if the service descriptor matches the specified interface, implementation, and lifetime; otherwise, false.</returns>
  public static bool ValidDependencyType<TServiceInterface, TServiceConcrete>(ServiceDescriptor serviceDescriptor, ServiceLifetime serviceLifetime)
  {
    // Validates Service Interface, Concrete and Lifetime
    var validServiceType = serviceDescriptor.ServiceType == typeof(TServiceInterface);
    var validImpmentationType = serviceDescriptor.ImplementationType == typeof(TServiceConcrete);
    var validServiceLifetime = serviceDescriptor.Lifetime == serviceLifetime;

    return validServiceType && validImpmentationType && validServiceLifetime;
  }
}