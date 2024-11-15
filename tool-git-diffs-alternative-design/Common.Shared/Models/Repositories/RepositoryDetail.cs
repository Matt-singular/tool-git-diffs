namespace Common.Shared.Models.Repositories;

using System;

/// <summary>
/// Contains in-depth details about a repository
/// </summary>
public class RepositoryDetail
{
  public int Age { get; set; }
  public int CodeAdded { get; set; }
  public int CodeDeleted { get; set; }
  public DateTimeOffset CreatedOn { get; set; }
  public required RepositoryVisibility Visibility { get; set; }
}