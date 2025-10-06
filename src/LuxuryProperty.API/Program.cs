using DotNetEnv;
using LuxuryProperty.Application.Services;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;
using LuxuryProperty.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

// Cargar variables del archivo .env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var mongoConnection = Environment.GetEnvironmentVariable("MONGO_URI");
var mongoDatabaseName = Environment.GetEnvironmentVariable("MONGO_DBNAME");
builder.Services.AddSingleton(new MongoDbContext(mongoConnection!, mongoDatabaseName!));

// Repositorios
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

// Servicios
builder.Services.AddScoped<PropertyService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Luxury Property API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
