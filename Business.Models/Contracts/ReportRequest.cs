namespace Business.Models.Contracts;

using Common.Shared.Models;

/// <summary>
/// The request object to get the list of differences between two specific commits, branches or tags
/// for specified repositories exported into a report file
/// </summary>
public record ReportRequest : IRepositoryDiffRequest
{
  /// <summary>
  /// The file name of the report
  /// </summary>
  public required string FileName { get; set; }

  /// <inheritdoc/>
  public required string RepositoryName { get; set; }

  /// <inheritdoc/>
  public required string RepositoryOwner { get; set; }

  /// <inheritdoc/>

  public required string FromReference { get; set; }

  /// <inheritdoc/>
  public required string ToReference { get; set; }
}