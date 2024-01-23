using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using SDHDotNetCore.MinimalApi.EFDbContext;
using SDHDotNetCore.MinimalApi.Features.Blog;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;
using System.Text.Json.Serialization;

//to get the project name
string projectName = Assembly.GetEntryAssembly()?.GetName()?.Name;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.
		MSSqlServer(
			connectionString: "Server=DESKTOP-DDE6MVJ\\TESTINGSDH;Database=TestDb;User ID=sa;Password=Sdh@1234;TrustServerCertificate=True;",
			sinkOptions: new MSSqlServerSinkOptions
			{
				TableName = "LogEventsForSerilog",
				AutoCreateSqlTable = true
			})
	//File($"logs/{projectName}.txt", rollingInterval: RollingInterval.Hour, fileSizeLimitBytes: 1024)
	.CreateLogger();

try
{
	logger.Info("Starting web application");

	var builder = WebApplication.CreateBuilder(args);

	builder.Host.UseSerilog();
	builder.Logging.ClearProviders();

	//Json CamelCase off
	builder.Services.ConfigureHttpJsonOptions(opt =>
	{
		opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		opt.SerializerOptions.PropertyNamingPolicy = null;
	});

	// Add services to the container.
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	builder.Services.AddDbContext<AppDbContext>(opt =>
	{
		opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
	},
	ServiceLifetime.Transient,
	ServiceLifetime.Transient);

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.AddBlogService();

	app.Run();

}
catch (Exception ex)
{
	//Log.Fatal(ex, "Application terminated unexpectedly");
	logger.Error("Application started fail.. => {ex}", ex.Message);
}
finally
{
	Log.CloseAndFlush();
}

