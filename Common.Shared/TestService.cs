namespace Common.Shared;

using Common.Shared.Config;
using Microsoft.Extensions.Options;

public class TestService : ITestService // TODO: testing settings
{
  private readonly SecretSettings secretSettings;

  public TestService(IOptions<SecretSettings> secretSettings)
  {
    this.secretSettings = secretSettings.Value;
  }

  public void Process()
  {
    var tmp = this.secretSettings;
  }
}

public interface ITestService
{
  public void Process();
}