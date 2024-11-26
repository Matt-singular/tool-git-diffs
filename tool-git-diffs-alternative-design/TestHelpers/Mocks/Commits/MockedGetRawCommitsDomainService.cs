namespace TestHelpers.Mocks.Commits;

using Common.Shared.Models.Commits;
using Common.Shared.Services.Commits.GetRawCommits;
using NSubstitute;

/// <summary>
/// A mocked instance of <see cref="IGetRawCommitsDomainService"/>
/// </summary>
public class MockedGetRawCommitsDomainService
{
  /// <summary>
  /// The mocked instance of the service
  /// </summary>
  public IGetRawCommitsDomainService Service { get; set; } = MockHelpers.MockService<IGetRawCommitsDomainService>();

  /// <summary>
  /// Substitutes the service's response for any input parameters
  /// </summary>
  /// <param name="rawCommitDetails">The response to substitute for</param>
  /// <returns>The service instance to allow substitution chains</returns>
  public IGetRawCommitsDomainService SubstituteForAny(List<RawCommitDetails> rawCommitDetails)
  {
    this.Service.ProcessAsync(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(rawCommitDetails));
    this.Service.ProcessAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(rawCommitDetails));

    return this.Service;
  }

  /// <summary>
  /// Substitutes the service's response for specific input parameters
  /// </summary>
  /// <param name="rawCommitDetails">The response to substitute for</param>
  /// <param name="repositoryId">The mocked repository id</param>
  /// <param name="fromReference">The mocked git reference to pull from</param>
  /// <param name="toReference">The mocked git reference to pull until</param>
  /// <returns>The service instance to allow substitution chains</returns>
  public IGetRawCommitsDomainService SubstituteForInput(List<RawCommitDetails> rawCommitDetails, long repositoryId, string fromReference, string toReference)
  {
    this.Service.ProcessAsync(repositoryId, fromReference, toReference).Returns(Task.FromResult(rawCommitDetails));

    return this.Service;
  }

  /// <summary>
  /// Substitutes the service's response for specific input parameters
  /// </summary>
  /// <param name="rawCommitDetails">The response to substitute for</param>
  /// <param name="repositoryName">The mocked repository name</param>
  /// <param name="repositoryOwner">The mocked repository owner</param>
  /// <param name="fromReference">The mocked git reference to pull from</param>
  /// <param name="toReference">The mocked git reference to pull until</param>
  public IGetRawCommitsDomainService SubstituteForInput(List<RawCommitDetails> rawCommitDetails, string repositoryName, string repositoryOwner,
    string fromReference, string toReference)
  {
    this.Service.ProcessAsync(repositoryName, repositoryOwner, fromReference, toReference).Returns(Task.FromResult(rawCommitDetails));

    return this.Service;
  }
}