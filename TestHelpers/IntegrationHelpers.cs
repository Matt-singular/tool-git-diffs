namespace TestHelpers;

/// <summary>
/// Provides a helping hand for integration tests.
/// </summary>
public static class IntegrationHelpers
{
  /// <summary>
  /// Represents a valid 'from' tag reference for testing.
  /// </summary>
  public static readonly string ValidFromTagReference = "12.2.0";

  /// <summary>
  /// Represents a valid 'to' tag reference for testing.
  /// </summary>
  public static readonly string ValidToTagReference = "12.2.1";

  /// <summary>
  /// Represents a non-existent reference for testing.
  /// </summary>
  public static readonly string NonExistantReference = "Non_Existant_Branch";
}
