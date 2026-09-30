using Microsoft.EntityFrameworkCore;
using MiniEHR_withDocker.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Read the connection string from configuration (appsettings or env var)
var connectionString = builder.Configuration.GetConnectionString("MiniEhrDb")
    ?? throw new InvalidOperationException("Connection string 'MiniEhrDb' is not configured.");


builder.Services.AddDbContext<MiniEhrDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthorization();
app.MapControllers();

app.Run();