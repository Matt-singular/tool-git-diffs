namespace Application.GUI;

using Common.Shared;
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
    builder.Services.AddCommonSharedServices();

    // TODO: testing things
    var serviceProvider = builder.Services.BuildServiceProvider();
    var testService = serviceProvider.GetService<ITestService>();
    testService.Process();

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
}