namespace Business.Domain.Commits;

using System.Threading.Tasks;
using Business.Models.Commits.GetRawCommits;

/// <inheritdoc cref="IGetRawCommits"/>
public class GetRawCommits : IGetRawCommits
{
  /// <inheritdoc/>
  public Task<GetRawCommitsResponse> ProcessAsync(GetRawCommitsRequest request)
  {
    throw new NotImplementedException();
  }
}