using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.MinimalApi.EFDbContext;
using SDHDotNetCore.MinimalApi.Features.Blog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

