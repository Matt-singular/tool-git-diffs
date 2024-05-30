using ApplicationConsole.Base;
using ApplicationConsole.Errors;
using ConfigExtraction.Base;
using Microsoft.Extensions.DependencyInjection;
using ReferenceExtraction.Base;

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