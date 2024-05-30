using ApplicationConsole.ConsoleHelpers;
using ApplicationConsole.Dependency;
using ApplicationConsole.Errors;
using Microsoft.Extensions.Hosting;

// 1) Application Builder
var host = Host.CreateDefaultBuilder(args);

// 2) Application Configuration
host.SetupApplicationConfiguration();

// 3) Set up DI container
host.SetupProjectServices();
var serviceProvider = host.Build().Services;

// 4) Exception Handling 
ExceptionMiddleware.SetupGlobalExceptionHandler();

// 5) User Input - build name, from, and to references
var build = ConsoleHelpers.PromptUserInput("Please enter a name for the build");
var from = ConsoleHelpers.PromptUserInput("Please enter a reference to pull from");
var to = ConsoleHelpers.PromptUserInput("Please enter a reference to pull until");

// 6) Execute diff generation
//var diffs = serviceProvider.ExecuteDiffGeneration(build, from, to);
serviceProvider.ExecuteDiffGeneration(build, from, to);

// 7) Ensure the Console remains open
ConsoleHelpers.KeepConsoleOpen();