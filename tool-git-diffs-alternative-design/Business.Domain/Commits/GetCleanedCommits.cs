namespace Business.Domain.Commits;

using System.Threading.Tasks;
using Business.Models.Commits.GetCleanedCommits;

/// <inheritdoc cref="IGetCleanedCommits"/>
public class GetCleanedCommits : IGetCleanedCommits
{
  /// <inheritdoc/>
  public Task<GetCleanedCommitsResponse> ProcessAsync(GetCleanedCommitsRequest request)
  {
    throw new NotImplementedException();
  }
}