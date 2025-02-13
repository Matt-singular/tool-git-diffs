namespace Application.API;

using System.Diagnostics.CodeAnalysis;
using Common.Shared.Extensions;

/// <summary>
/// Application.API Startup logic
/// </summary>
[ExcludeFromCodeCoverage]
public class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Configurations
    builder.Configuration.AddCommonSharedConfiguration();
    builder.Services.ConfigureCommonSettings(builder.Configuration);

    // Dependency Injection
    builder.Services.AddCommonSharedServices();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}