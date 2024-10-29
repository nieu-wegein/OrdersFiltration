using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersFiltration;
using OrdersFiltration.Services;
using OrdersFiltration.Services.Interfaces;
using OrdersFiltration.Tools;
using Serilog;



using var host = BuildHost();
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;
CreateLogger();

try
{
	await services.GetRequiredService<Application>().Run(args);
}
catch (Exception e)
{
	ConsoleTools.LogError("Приложение завершилось с ошибкой: " + e.Message);
	Log.Logger.Warning($"Aplication has crashed, Error message: {e.Message}, Stack trace: {e.StackTrace}");
}


IHost BuildHost()
{
	return Host.CreateDefaultBuilder().ConfigureServices(services =>
	{
		services.AddSingleton<IDataService, OrdersFileService>()
				.AddSingleton<Application>();
	}).UseSerilog().Build();
}

void CreateLogger()
{
	var config = services.GetRequiredService<IConfiguration>();

	Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(new ConfigurationBuilder().Build())
	.WriteTo.File(args.Length == 0 ? config.GetValue<string>("Logging:Destination") : args[0])
	.CreateLogger();
}