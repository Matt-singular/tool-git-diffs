namespace Business.Domain.Repositories;

using System.Threading.Tasks;
using Business.Models.Repositories.GetRepositoryList;

/// <inheritdoc cref="IGetRepositoryList"/>
public class GetRepositoryList : IGetRepositoryList
{
  /// <inheritdoc/>
  public Task<GetRepositoryListResponse> ProcessAsync(GetRepositoryListRequest request)
  {
    throw new NotImplementedException();
  }
}