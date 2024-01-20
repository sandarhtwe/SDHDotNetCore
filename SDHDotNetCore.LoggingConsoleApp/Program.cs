// See https://aka.ms/new-console-template for more information
using Serilog;
using System.Reflection;

Console.WriteLine("Hello, World!");

//to get the project name
string projectName = Assembly.GetEntryAssembly()?.GetName()?.Name;

Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.WriteTo.File($"logs/{projectName}.txt", rollingInterval: RollingInterval.Day)
			.CreateLogger();

Log.Information("Hello, world!");

int a = 10, b = 0;
try
{
	Log.Debug("Dividing {A} by {B}", a, b);
	Console.WriteLine(a / b);
}
catch (Exception ex)
{
	Log.Error(ex, "Something went wrong");
}
finally
{
	await Log.CloseAndFlushAsync();
}