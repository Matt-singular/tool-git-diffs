namespace SharedTestDependencies;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Various test project Helper methods
/// </summary>
public static class Helpers
{
  /// <summary>
  /// Validate the DI type
  /// </summary>
  /// <typeparam name="TServiceInterface">The Service interface type</typeparam>
  /// <typeparam name="TServiceConcrete">The Service implementation type</typeparam>
  /// <param name="serviceDescriptor"></param>
  /// <param name="serviceLifetime">The service lifetime (Singleton, Scoped, Transient)</param>
  /// <returns>True if the service matches the specified interface, implementation and lifetime types</returns>
  public static bool ValidDependencyType<TServiceInterface, TServiceConcrete>(ServiceDescriptor serviceDescriptor, ServiceLifetime serviceLifetime)
  {
    var validServiceType = serviceDescriptor.ServiceType == typeof(TServiceInterface);

    var validImpmentationType = serviceDescriptor.ImplementationType == typeof(TServiceConcrete);

    var validServiceLifetime = serviceDescriptor.Lifetime == serviceLifetime;

    return validServiceType && validImpmentationType && validServiceLifetime;
  }
}