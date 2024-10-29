using Microsoft.Extensions.Logging;
using OrdersFiltration.Models;
using OrdersFiltration.Services.Interfaces;
using OrdersFiltration.Tools;
using System.ComponentModel.DataAnnotations;

namespace OrdersFiltration
{
	public class Application
	{
		private readonly InputModel _input;
		private readonly IDataService _dataService;
		private readonly ILogger<Application> _logger;

		public Application(IDataService dataService, ILogger<Application> logger)
		{
			_input = new InputModel();
			_dataService = dataService;
			_logger = logger;
		}

		public async Task Run(string[] args)
		{
			_logger.LogInformation("The application has started");

			if (!await _dataService.LoadDataAsync())
				ConsoleTools.LogError("Не удалось загрузить данные в программу");

			ReadInput();
			List<Order> orders = Filter();

			if (!await _dataService.SaveDataAsync(orders, args.Length < 2 ? null : args[1]))
				ConsoleTools.LogError("Не удалось сохранить данные в файл");
			else
				ConsoleTools.LogInfo("Данные успешно отфильтрованы в файл");


			_logger.LogInformation("The application has stopped");
		}


		private void ReadInput()
		{
			var errors = new List<ValidationResult>();
			var context = new ValidationContext(_input);

			do
			{
				foreach (var e in errors)
				{
					ConsoleTools.LogError(e.ErrorMessage);
					_logger.LogWarning($"Incorrect data provided; Validation error: {e.ErrorMessage}");
				}
				errors.Clear();

				ConsoleTools.LogInfo("Введите данные для фильтрации");

				Console.Write("Район: ");
				_input.District = DistrictTools.TryParse(Console.ReadLine());

				Console.Write("Время первой доставки: ");
				_input.DateTime = DateTime.TryParse(Console.ReadLine(), out var time) ? time : default(DateTime);

			}
			while (!Validator.TryValidateObject(_input, context, errors, true));

			_logger.LogInformation($"Data entered: District: {_input.District}, First delivery time: {_input.DateTime}");
		}

		private List<Order> Filter()
		{
			List<Order> orders = _dataService.Orders.Where(order => order.District == _input.District &&
					order.DeliveryDate >= _input.DateTime && order.DeliveryDate <= _input.DateTime.AddMinutes(30)).ToList();

			_logger.LogInformation($"Found {orders.Count()} orders matching the provided input parameters");

			return orders;
		}

	}
}
