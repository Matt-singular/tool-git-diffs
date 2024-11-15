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
}