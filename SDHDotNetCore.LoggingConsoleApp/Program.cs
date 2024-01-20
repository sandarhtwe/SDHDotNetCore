// See https://aka.ms/new-console-template for more information
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;

Console.WriteLine("Hello, World!");

//to get the project name
string projectName = Assembly.GetEntryAssembly()?.GetName()?.Name;

Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.WriteTo./*File($"logs/{projectName}.txt", rollingInterval: RollingInterval.Day)*/
				MSSqlServer(
					connectionString: "Server=DESKTOP-DDE6MVJ\\TESTINGSDH;Database=TestDb;User ID=sa;Password=Sdh@1234;TrustServerCertificate=True;",
					sinkOptions: new MSSqlServerSinkOptions
					{
						TableName = "LogEvents",
						AutoCreateSqlTable = true
					})
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