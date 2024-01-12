using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.RestApi.EFDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    //{
    //    DataSource = "DESKTOP-DDE6MVJ\\TESTINGSDH",
    //    InitialCatalog = "HKSDotNetCore",
    //    UserID = "Sa",
    //    Password = "Sdh@1234",
    //    TrustServerCertificate = true
    //};
    //opt.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
    string connectionString = builder.Configuration.GetConnectionString("DbConnection");
    opt.UseSqlServer(connectionString);
});

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
