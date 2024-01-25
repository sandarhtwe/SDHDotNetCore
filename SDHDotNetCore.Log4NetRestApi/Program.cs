using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddLog4net();

		try
		{
			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
		catch (Exception ex)
		{
			// Handle the exception or log it using a fallback logging mechanism
			Console.WriteLine($"An error occurred during application startup: {ex.Message}");
			throw; // Rethrow the exception to terminate the application startup
		}
		finally
		{
			// Optionally, add cleanup logic here
		}
	}
}

public static class Log4netExtensions
{
	public static void AddLog4net(this IServiceCollection services)
	{
		XmlConfigurator.Configure(new FileInfo("log4net.config"));
		services.AddSingleton(LogManager.GetLogger(typeof(Program)));
	}
}