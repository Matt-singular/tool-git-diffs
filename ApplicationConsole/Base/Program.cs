using ApplicationConsole.Base;
using ConfigExtraction.Base;
using Microsoft.Extensions.DependencyInjection;

// Application startup - set up DI container
var serviceProvider = new ServiceCollection()
    .AddApplicationConsoleServices()
    .AddConfigExtractionServices()
    .BuildServiceProvider();

// Call the application orchestration logic
var applicationOrchestration = serviceProvider.GetRequiredService<ApplicationConsole.IOrchestration>();
applicationOrchestration.Process();

// Done - ensure the console remains open
Console.Write("\nPress Enter to exit...");
_ = Console.ReadLine();

/* TODO LIST */
/* 1) Currently don't use DI anywhere, consider adding it
 * 2) XXX
 * 3) XXX
*/