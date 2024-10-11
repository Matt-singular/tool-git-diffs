namespace Application.GUI;

using Business.Infrastructure.Extensions;
using Common.Shared.Extensions;
using Microsoft.Extensions.Logging;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();

    // Configurations
    builder.Configuration.SetupCommonSharedConfiguration();
    builder.Services.SetupCommonSharedConfigSettings(builder.Configuration);

    // Dependency Injection
    builder.Services.AddMauiServices();
    builder.Services.AddCommonSharedServices();
    builder.Services.AddBusinessInfrastructureServices();

    // Maui App
    builder
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });

    // Logging
#if DEBUG
    builder.Logging.AddDebug();
#endif

    return builder.Build();
  }

  private static IServiceCollection AddMauiServices(this IServiceCollection services)
  {
    services.AddSingleton<MainPage>();

    return services;
  }
}