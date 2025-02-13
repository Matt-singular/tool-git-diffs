namespace Application.GUI;

using System.Diagnostics.CodeAnalysis;
using Application.GUI.Extensions;
using Common.Shared.Extensions;
using Microsoft.Extensions.Logging;

/// <summary>
/// Application.GUI Startup logic
/// </summary>
[ExcludeFromCodeCoverage]
public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();

    // Configurations
    builder.Configuration.AddCommonSharedConfiguration();
    builder.Services.ConfigureCommonSettings(builder.Configuration);

    // Dependency Injection
    builder.Services.AddMauiPages();
    builder.Services.AddCommonSharedServices();

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

    // Application Startup
    var app = builder.Build();

    return app;
  }
}