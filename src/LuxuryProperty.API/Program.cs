using DotNetEnv;
using LuxuryProperty.Application.Services;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Data;
using LuxuryProperty.Infrastructure.Database;
using LuxuryProperty.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton(sp =>
{
  var logger = sp.GetRequiredService<ILogger<MongoDbContext>>();
  var mongoConnection = Environment.GetEnvironmentVariable("MONGO_URI") ?? throw new Exception("The MONGO_URI environment variable is not set.");
  var mongoDatabaseName = Environment.GetEnvironmentVariable("MONGO_DBNAME") ?? throw new Exception("The MONGO_DBNAME environment variable is not set.");
  return new MongoDbContext(mongoConnection, mongoDatabaseName, logger);
});

// Repositorios
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();

// Servicios
builder.Services.AddScoped<PropertyService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Luxury Property API", Version = "v1" });
});

var app = builder.Build();

// If you want to new data for testing, uncomment the following lines
using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

  var propertiesSeeder = new PropertySeeder(dbContext);
  await propertiesSeeder.SeedAsync();

  var propertyImagesSeeder = new PropertyImagesSeeder(dbContext, scope.ServiceProvider.GetRequiredService<ILogger<PropertyImagesSeeder>>());
  await propertyImagesSeeder.SeedAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();

  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
