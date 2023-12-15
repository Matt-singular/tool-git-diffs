namespace ConfigExtraction.Services;

using ConfigExtraction.Models;

public record ValidateConfig(ConfigModel Config) : IValidateConfig
{
  public bool Process()
  {
    throw new NotImplementedException();
  }

  public bool CheckIfDefault()
  {
    throw new NotImplementedException();
  }

  public bool CheckDiffRangeSelection()
  {
    throw new NotImplementedException();
  }

  public bool CheckCommitReferences()
  {
    throw new NotImplementedException();
  }
}

public interface IValidateConfig
{
  public ConfigModel Config { get; init; }
  public bool Process();
  public bool CheckIfDefault(); // Model not default (will need to modify ReadConfig)
  public bool CheckDiffRangeSelection();
  public bool CheckCommitReferences();
}