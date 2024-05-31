using ApplicationConsole.ConsoleHelpers;
using ApplicationConsole.Dependency;
using ApplicationConsole.Errors;
using Configuration.Dependency;
using Microsoft.Extensions.Hosting;

// 1) Application Builder
var host = Host.CreateDefaultBuilder(args);

// 2) Application Configuration
host.SetupApplicationConfiguration<Program>();

// 3) Set up DI container
host.SetupProjectServices();
var serviceProvider = host.Build().Services;

// 4) Exception Handling 
ExceptionMiddleware.SetupGlobalExceptionHandler();
serviceProvider.ValidateConfigurations();

// 5) User Input - build name, from, and to references
var build = ConsoleHelpers.PromptUserInput("Please enter a name for the build");
var from = ConsoleHelpers.PromptUserInput("Please enter a reference to pull from");
var to = ConsoleHelpers.PromptUserInput("Please enter a reference to pull until");

// 6) Ensure the Console remains open
ConsoleHelpers.KeepConsoleOpen();