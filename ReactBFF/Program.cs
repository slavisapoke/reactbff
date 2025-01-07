using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;
using Initialization;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    builder.WithOrigins("http://localhost:3000")  // Allow localhost:3000
           .AllowAnyMethod()                     // Allow any HTTP method (GET, POST, etc.)
           .AllowAnyHeader());
});

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CosmosDBSettings>(
    builder.Configuration.GetSection(CosmosDBSettings.SETTINGS_NAME)); 

builder.Logging.ClearProviders(); // Remove default loggers
builder.Logging.AddConsole(); // Add console logging
builder.Logging.AddAzureWebAppDiagnostics();

builder.Services.ConfigureServices();

var app = builder.Build();
 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowLocalhost3000");

app.MapControllers();

app.Run();
