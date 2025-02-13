namespace Business.Domain;

using Business.Domain.Commits;
using Business.Domain.Reports;
using Business.Domain.Repositories;
using Business.Models.Commits.GetCleanedCommits;
using Business.Models.Commits.GetRawCommits;
using Business.Models.Reports.GetCleanedExcelReport;
using Business.Models.Reports.GetRawExcelReport;
using Business.Models.Repositories.GetRepositoryDetail;
using Business.Models.Repositories.GetRepositoryList;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Business domain startup extensions
/// </summary>
public static class StartupExtensions
{
  /// <summary>
  /// Adds the Business domain startup extensions
  /// </summary>
  /// <param name="services">The application's service collection</param>
  /// <returns>The configured services</returns>
  public static IServiceCollection AddBusinessDomainServices(this IServiceCollection services)
  {
    // Register Business.Domain services
    services.AddCommitsDomainServices();
    services.AddReportsDomainServices();
    services.AddRepositoriesDomainServices();

    return services;
  }

  private static IServiceCollection AddCommitsDomainServices(this IServiceCollection services)
  {
    services.AddScoped<IGetCleanedCommits, GetCleanedCommits>();
    services.AddScoped<IGetRawCommits, GetRawCommits>();

    return services;
  }

  private static IServiceCollection AddReportsDomainServices(this IServiceCollection services)
  {
    services.AddScoped<IGetCleanedExcelReport, GetCleanedExcelReport>();
    services.AddScoped<IGetRawExcelReport, GetRawExcelReport>();

    return services;
  }

  private static IServiceCollection AddRepositoriesDomainServices(this IServiceCollection services)
  {
    services.AddScoped<IGetRepositoryDetail, GetRepositoryDetail>();
    services.AddScoped<IGetRepositoryList, GetRepositoryList>();

    return services;
  }
}