using System.Reflection;
using Business.Domain;
using Common.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Configuration.AddCommonSharedConfiguration();
builder.Services.ConfigureCommonSettings(builder.Configuration);

builder.Services.AddCommonSharedServices();
builder.Services.AddBusinessDomainServices();

builder.Services.AddSwaggerGen(options =>
{
  // Add Comments to Swagger
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.SuccessMessage();

app.Run();