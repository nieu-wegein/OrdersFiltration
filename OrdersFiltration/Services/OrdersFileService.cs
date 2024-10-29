using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrdersFiltration.Enums;
using OrdersFiltration.Models;
using OrdersFiltration.Services.Interfaces;
using OrdersFiltration.Tools;
using System.Text.Json;

namespace OrdersFiltration.Services
{

	public class OrdersFileService : IDataService
	{
		private readonly IConfiguration _config;
		private readonly ILogger<OrdersFileService> _logger;
		public List<Order> Orders { get; set; }

		public OrdersFileService(IConfiguration config, ILogger<OrdersFileService> logger)
		{
			_config = config;
			_logger = logger;
		}

		public async Task<bool> LoadDataAsync()
		{
			try
			{
				using FileStream fs = new FileStream(_config.GetValue<string>("Data:Source"), FileMode.Open);
				var options = new JsonSerializerOptions { Converters = { new DateTimeConverter() }, PropertyNameCaseInsensitive = true };

				Orders = await JsonSerializer.DeserializeAsync<List<Order>>(fs, options);

				_logger.LogInformation($"Successfully loaded data from {fs.Name}");
				return true;
			}
			catch
			{
				Orders = new List<Order>();

				_logger.LogWarning($"Failed to load data");
				return false;
			}
		}

		public async Task<bool> SaveDataAsync(List<Order> orders, string path)
		{
			try
			{
				path ??= _config.GetValue<string>("Data:Destination");
				var dir = Path.GetDirectoryName(path);

				if (!String.IsNullOrEmpty(dir))
					Directory.CreateDirectory(Path.GetDirectoryName(path));

				using FileStream fs = new FileStream(path, FileMode.Create);
				await JsonSerializer.SerializeAsync(fs, orders, new JsonSerializerOptions { Converters = { new DateTimeConverter() }, WriteIndented = true, });

				_logger.LogInformation($"Successfully saved data to {fs.Name}");
				return true;
			}
			catch
			{
				_logger.LogError($"Failed to save data");
				return false;
			}
		}
	}
}
