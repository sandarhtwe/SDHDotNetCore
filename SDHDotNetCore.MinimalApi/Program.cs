using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.MinimalApi.EFDbContext;
using SDHDotNetCore.MinimalApi.Features.Blog;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;
using System.Text.Json.Serialization;

//to get the project name
string projectName = Assembly.GetEntryAssembly()?.GetName()?.Name;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.
		MSSqlServer(
			connectionString: "Server=DESKTOP-DDE6MVJ\\TESTINGSDH;Database=TestDb;User ID=sa;Password=Sdh@1234;TrustServerCertificate=True;",
			sinkOptions: new MSSqlServerSinkOptions
			{
				TableName = "LogEventsForMinimalApi",
				AutoCreateSqlTable = true
			})
	//File($"logs/{projectName}.txt", rollingInterval: RollingInterval.Hour, fileSizeLimitBytes: 1024)
	.CreateLogger();

try
{
	Log.Information("Starting web application");

	var builder = WebApplication.CreateBuilder(args);

	builder.Host.UseSerilog();

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
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}

