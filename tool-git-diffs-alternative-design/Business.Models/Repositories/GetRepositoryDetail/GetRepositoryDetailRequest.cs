namespace Business.Models.Repositories.GetRepositoryDetail;

using Common.Shared.Models.Exceptions;

/// <summary>
/// Contains the data needed to lookup a specific repository
/// </summary>
public class GetRepositoryDetailRequest
{
  public long? RepositoryId { get; set; }

  public string? RepositoryOwner { get; set; }
  public string? RepositoryName { get; set; }

  /// <summary>
  /// Determines whether the model is valid
  /// </summary>
  /// <returns>True if the model is valid, otherwise False</returns>
  public bool ValidateModel()
  {
    var errorMessage = $"Bad Request for {nameof(GetRepositoryDetailRequest)}";

    if (this.RepositoryId is null && this.RepositoryName is null)
    {
      var error = $"{errorMessage}\nYou must specify a repository id or name";
      throw new BadRequestException(error);
    }

    if (this.RepositoryName is not null && this.RepositoryOwner is null)
    {
      var error = $"{errorMessage}\nYou must specify the repository owner when doing a lookup using the repository name";
      throw new BadRequestException(error);
    }

    return true;
  }
}