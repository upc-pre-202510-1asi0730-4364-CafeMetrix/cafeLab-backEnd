using CafeLab.API.Shared.Domain.Repositories;

//builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
using CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

//AQUÍ ESCOGEMOS LA CONEXIÓN DE DB QUE QUEREMOS USAR ---------------------------------------------
//Porque cada motor de db tiene su propia cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null) throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Que levante la db, sea desarrollo o producción
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});
//------------------------------------------------------------------------------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection:
// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// otros bounded contexts...

// Profiles Bounded Context

// IAM Bounded Context

// TokenSettings Configuration


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Authorization Middleware to Pipeline
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();