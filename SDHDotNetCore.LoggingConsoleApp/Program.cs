﻿// See https://aka.ms/new-console-template for more information
using log4net;
using log4net.Config;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;

internal class Program
{
	private static readonly ILog log4netLogger = LogManager.GetLogger(typeof(Program));
	private static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		//to get the project name
		string projectName = Assembly.GetEntryAssembly()?.GetName()?.Name;

		//Log.Logger = new LoggerConfiguration()
		//			.MinimumLevel.Debug()
		//			.WriteTo.Console()
		//			.WriteTo./*File($"logs/{projectName}.txt", rollingInterval: RollingInterval.Day)*/
		//				MSSqlServer(
		//					connectionString: "Server=DESKTOP-DDE6MVJ\\TESTINGSDH;Database=TestDb;User ID=sa;Password=Sdh@1234;TrustServerCertificate=True;",
		//					sinkOptions: new MSSqlServerSinkOptions
		//					{
		//						TableName = "LogEvents",
		//						AutoCreateSqlTable = true
		//					})
		//			.CreateLogger();

		// Configure log4net
		var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
		XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));

		//Log.Information("Hello, world!");

		// Log using log4net
		log4netLogger.Info("Hello, world! (log4net)");


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
			// Close and flush log4net
			LogManager.Shutdown();

			// Close and flush Serilog
			Log.CloseAndFlush();
		}
	}
}