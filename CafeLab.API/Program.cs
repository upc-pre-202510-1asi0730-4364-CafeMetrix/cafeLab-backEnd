using Microsoft.EntityFrameworkCore;
using CafeLab.API.CuppingSessions.Domain.Repositories;
using CafeLab.API.CuppingSessions.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.CuppingSessions.Application;
using CafeLab.API.CostosLote.Domain.Repositories;
using CafeLab.API.CostosLote.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.CostosLote.Application.Internal.CommandServices;
using CafeLab.API.MovimientosInventario.Domain.Repositories;
using CafeLab.API.MovimientosInventario.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.MovimientosInventario.Application;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MySql.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CafeLab API",
        Version = "v1",
        Description = "Backend"
    });
});

// Configuración de Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The connection string 'DefaultConnection' was not found.");
}
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

// Registrar servicios del Bounded Context CuppingSessions
builder.Services.AddScoped<ICuppingSessionRepository, CuppingSessionRepository>();
builder.Services.AddScoped<ICuppingSessionApplicationService, CuppingSessionApplicationService>();

// Registrar servicios del Bounded Context CostosLote
builder.Services.AddScoped<ICostoLoteRepository, CostoLoteRepository>();
builder.Services.AddScoped<ICostoLoteApplicationService, CostoLoteApplicationService>();

// Registrar servicios del Bounded Context MovimientosInventario
builder.Services.AddScoped<IMovimientoInventarioRepository, MovimientoInventarioRepository>();
builder.Services.AddScoped<IMovimientoInventarioApplicationService, MovimientoInventarioApplicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CafeLab API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
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