using YourCorporation.Bootstrapper;
using YourCorporation.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
  .AddUserSecrets<Program>()
  .AddEnvironmentVariables()
  .Build();

var assemblies = ModuleLoader.LoadAssemblies();
var modules = ModuleLoader.LoadModules(assemblies);

builder.Services.AddModularInfrastructure(assemblies, configuration);
foreach (var module in modules)
{
    module.Register(builder.Services, configuration);
}

var app = builder.Build();

app.UseModularInfrastructure(assemblies);
foreach (var module in modules)
{
    module.ConfigureModule(app);
}

app.Run();