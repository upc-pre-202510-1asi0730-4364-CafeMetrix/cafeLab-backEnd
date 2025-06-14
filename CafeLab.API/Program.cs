using Microsoft.EntityFrameworkCore;
using CafeLab.API.CuppingSessions.Domain.Repositories;
using CafeLab.API.CuppingSessions.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.CuppingSessions.Application;
using CafeLab.API.CostosLote.Domain.Repositories;
using CafeLab.API.CostosLote.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.CostosLote.Application;
using CafeLab.API.MovimientosInventario.Domain.Repositories;
using CafeLab.API.MovimientosInventario.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.MovimientosInventario.Application;
using CafeLab.API.Data;
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
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Aplicar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
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