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
    var errorMessage = $"Bad Request for {nameof(GetRepositoryListRequest)}";

    if (this.OrganisationName is null && this.UserName is null)
    {
      var error = $"{errorMessage}\nYou must specify an organisation or user";
      throw new BadRequestException(error);
    }

    if (this.OrganisationName is not null && this.UserName is not null)
    {
      var error = $"{errorMessage}\nYou cannot specify both an organisation and a user";
      throw new BadRequestException(error);
    }

    return true;
  }
}