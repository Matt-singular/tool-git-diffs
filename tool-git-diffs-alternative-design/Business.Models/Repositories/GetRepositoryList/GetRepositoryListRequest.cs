namespace Business.Models.Repositories.GetRepositoryList;

using Common.Shared.Models.Exceptions;

/// <summary>
/// Contains the data needed to get the list of repositories
/// </summary>
public class GetRepositoryListRequest
{
  public string? OrganisationName { get; set; }
  public string? UserName { get; set; }

  /// <summary>
  /// Determines whether the model is valid
  /// </summary>
  /// <returns>True if the model is valid, otherwise False</returns>
  public bool ValidateModel()
  {
    var notValid = this.OrganisationName is not null && this.UserName is not null;

    if (notValid)
    {
      throw new BadRequestException($"Bad Request for {nameof(GetRepositoryListRequest)}");
    }

    return true;
  }
}