using log4net.Config;
using log4net.Core;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace SDHDotNetCore.Log4NetRestApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			//_logger = logger;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			//_logger.LogInformation("Hello, this is the index!");
			_logger.LogInformation("This is an informational log.");
			_logger.LogError("This is an error log.");
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

	}
}