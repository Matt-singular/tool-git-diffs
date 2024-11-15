namespace Common.Shared.Models.Exceptions;

using System;

/// <summary>
/// Represents a failed GitHub API call (error code 500)
/// </summary>
public class GitHubApiException : Exception
{
  public GitHubApiException() : base("The GitHub API operation failed.")
  {
  }

  public GitHubApiException(string message) : base(message)
  {
  }

  public GitHubApiException(string message, Exception innerException) : base(message, innerException)
  {
  }
}