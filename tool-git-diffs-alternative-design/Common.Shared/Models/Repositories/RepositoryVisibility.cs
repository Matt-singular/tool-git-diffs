namespace Common.Shared.Models.Repositories;

/// <summary>
/// The different repository visibilities
/// </summary>
public enum RepositoryVisibility
{
  /// <summary>
  /// The repository is visible to all users
  /// </summary>
  Public,

  /// <summary>
  /// The repository is only visible to the owner and invited users
  /// </summary>
  Private
}