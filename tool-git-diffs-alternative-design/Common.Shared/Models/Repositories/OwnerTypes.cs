namespace Common.Shared.Models.Repositories;

/// <summary>
/// The different types of repository owners
/// </summary>
public enum OwnerTypes
{
  /// <summary>
  /// The owner is a GitHub organisation
  /// </summary>
  Organisation,

  /// <summary>
  /// The owner is a GitHub user (individual)
  /// </summary>
  User
}