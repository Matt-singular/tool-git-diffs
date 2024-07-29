namespace Business.Infrastructure.Tests;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;

public static class BusinessInfrastructureTestHelper
{
  public static IOptions<TSettings> MockOptionSettings<TSettings>(TSettings settings) where TSettings : class
  {
    // Method to wrap the provided setting values in an IOptions wrapper.
    var mockedOptions = Substitute.For<IOptions<TSettings>>();

    // Use the provided settings object
    mockedOptions.Value.Returns(settings);
    return mockedOptions;
  }

  public static TService MockDomainService<TService>() where TService : class
  {
    // Ensure the service provided is an interface as this allows proper mocking
    var isInterface = typeof(TService).IsInterface;
    if (!isInterface)
    {
      throw new ArgumentException("You must specify an interface when mocking.");
    }

    // Mock out the specified service & method
    var mockedService = Substitute.For<TService>();
    return mockedService;
  }

  public static TService MockDomainServiceResponse<TService, TRequest, TResponse>(this TService mockedService,
    Func<TService, Delegate> methodSelector, TResponse methodResponse) where TService : class
  {
    // Configure the service method
    var method = methodSelector(mockedService);
    method.DynamicInvoke(Arg.Any<TRequest>()).Returns(Task.FromResult(methodResponse));

    return mockedService;
  }

  public static TService MockDomainServiceResponse<TService, TRequest, TResponse>(this TService mockedService,
    Func<TService, Delegate> methodSelector, string path, string jsonFile) where TService : class
  {
    // Get data from JSON payload
    var methodResponse = ParseJsonFile<TResponse>(path, jsonFile);

    // Configure the service method
    var method = methodSelector(mockedService);
    method.DynamicInvoke(Arg.Any<TRequest>()).Returns(Task.FromResult(methodResponse));

    return mockedService;
  }

  public static TResponse? ParseJsonFile<TResponse>(string path, string jsonFile)
  {
    // Read the JSON file and deserialize it into the response type
    var jsonFilePath = Path.Combine(path, jsonFile);
    var jsonData = File.ReadAllText(jsonFilePath);
    var jsonObject = JsonConvert.DeserializeObject<TResponse>(jsonData);

    return jsonObject;
  }
}