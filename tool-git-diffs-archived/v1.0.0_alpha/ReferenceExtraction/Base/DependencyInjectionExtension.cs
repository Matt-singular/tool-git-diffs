﻿namespace ReferenceExtraction.Base;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReferenceExtraction.Services;

public static class DependencyInjectionExtension
{
  /// <summary>
  /// Add the ReferenceExtraction services
  /// </summary>
  /// <param name="serviceCollection"></param>
  /// <returns>The configured ServiceCollection</returns>
  public static ServiceCollection AddReferenceExtractionServices(this ServiceCollection serviceCollection)
  {
    // Singleton Services
    serviceCollection.TryAddSingleton<IOrchestration, Orchestration>();

    // Scoped Services
    serviceCollection.TryAddScoped<IGenerateRegexes, GenerateRegexes>();
    serviceCollection.TryAddScoped<IExtractReferences, ExtractReferences>();

    return serviceCollection;
  }
}