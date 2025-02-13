namespace Common.Shared.Models.Exceptions;

using System;

/// <summary>
/// Represents a bad request that has failed validations (error code 400)
/// </summary>
public class BadRequestException : Exception
{
  public BadRequestException() : base("The request is invalid.")
  {
  }

  public BadRequestException(string message) : base(message)
  {
  }

  public BadRequestException(string message, Exception innerException) : base(message, innerException)
  {
  }

  public static void ThrowIfNullOrEmpty(string? input, string parameterName)
  {
    if (string.IsNullOrEmpty(input))
    {
      throw new BadRequestException($"The parameter '{parameterName}' cannot be null or empty.");
    }
  }

  public static void ThrowIfNullOrEmpty<T>(IEnumerable<T>? collection, string parameterName)
  {
    if (collection is null || !collection.Any())
    {
      throw new BadRequestException($"The parameter '{parameterName}' cannot be null or empty.");
    }
  }
}