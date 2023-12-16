using ApplicationConsole.Base;
using ConfigExtraction.Base;
using ReferenceExtraction.Base;
using Microsoft.Extensions.DependencyInjection;

// Application startup - set up DI container
var serviceProvider = new ServiceCollection()
  .AddApplicationConsoleServices()
  .AddConfigExtractionServices()
  .AddReferenceExtractionServices()
  .BuildServiceProvider();

// Exception Handling 
ExceptionMiddleware.SetupGlobalExceptionHandler();

// Call the application orchestration logic
var applicationOrchestration = serviceProvider.GetRequiredService<ApplicationConsole.IOrchestration>();
applicationOrchestration.Process();

// Ensure the Console remains open
Console.Write("\nPress Enter to exit...");
_ = Console.ReadLine();