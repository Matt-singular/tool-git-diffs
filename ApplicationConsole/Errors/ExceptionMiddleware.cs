namespace ApplicationConsole.Errors;

using System;

/// <summary>
/// Catch all the thrown and unhandled exceptions to allow custom handling logic
/// </summary>
public static class ExceptionMiddleware
{
  public static void SetupGlobalExceptionHandler()
  {
    AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
    {
      // Extract the Exception and message
      var exception = args.ExceptionObject as Exception;
      var message = exception?.Message;

      // Write the error message
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write(message);

      // Reset the Console colour and keep it open
      Console.ResetColor();
#if RELEASE
      // Only block the console from auto closing on error if we're in Release Mode.
      // This allows for nice friendly error messages to be displayed when using the published exe
      // without slowing down the local development process.
      Console.ReadLine();
#endif
    };
  }
}