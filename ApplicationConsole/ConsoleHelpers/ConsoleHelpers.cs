namespace ApplicationConsole.ConsoleHelpers;

/// <summary>
/// This class provides helper methods for console operations.
/// </summary>
public static class ConsoleHelpers
{
  /// <summary>
  /// Keeps the console window open until the presses the Enter key.
  /// </summary>
  public static void KeepConsoleOpen()
  {
    // Ensure the Console remains open
    Console.ResetColor();
    Console.Write("\nPress Enter to exit...");
    _ = Console.ReadLine();
  }

  /// <summary>
  /// Prompts the user for input with a specified prompt message.
  /// Throws an exception if the user does not provide any input.
  /// </summary>
  /// <param name="prompt">The prompt message to display to the user.</param>
  /// <returns>The user's input as a string.</returns>
  /// <exception cref="ArgumentNullException">Thrown when no input is provided by the user.</exception>
  public static string PromptUserInput(string prompt)
  {
    Console.WriteLine(prompt);
    var answer = Console.ReadLine();
    Console.WriteLine();

    if (string.IsNullOrWhiteSpace(answer))
    {
      throw new ArgumentNullException("You must enter an answer.");
    }

    return answer;
  }
}