using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;
using Initialization;
using Model;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddAzureWebAppDiagnostics();

builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "logs-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    builder.WithOrigins("http://localhost:3000")  // Allow localhost:3000
           .AllowAnyMethod()                     // Allow any HTTP method (GET, POST, etc.)
           .AllowAnyHeader());
});

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CosmosDBSettings>(
    builder.Configuration.GetSection(CosmosDBSettings.SETTINGS_NAME));

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
