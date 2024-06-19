namespace ApplicationConsole.ErrorHandling;

/// <summary>
/// Provides helper methods for error handling.
/// </summary>
public static class ErrorsHelpers
{
  /// <summary>
  /// Gets the innermost exception message using recursion.
  /// </summary>
  /// <param name="exception">The exception to get the innermost message from.</param>
  /// <returns>The message of the innermost exception.</returns>
  public static string GetInnermostExceptionMessage(Exception exception)
  {
    // Base case
    if (exception.InnerException == null)
    {
      // Innermost exception message
      return exception.Message;
    }

    // Recursion case
    return GetInnermostExceptionMessage(exception.InnerException);
  }
}
