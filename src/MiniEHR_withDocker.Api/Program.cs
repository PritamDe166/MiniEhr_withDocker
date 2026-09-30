using Microsoft.EntityFrameworkCore;
using MiniEHR_withDocker.Api.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Read the connection string from configuration (appsettings or env var)
var connectionString = builder.Configuration.GetConnectionString("MiniEhrDb")
    ?? throw new InvalidOperationException("Connection string 'MiniEhrDb' is not configured.");


builder.Services.AddDbContext<MiniEhrDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<MiniEhrDbContext>("database", tags: ["ready"]);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = check => check.Tags.Contains("ready") });

app.Run();