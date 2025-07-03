using CafeLab.API.Profiles.Application.Internal.CommandServices;
using CafeLab.API.Profiles.Application.Internal.QueryServices;
using CafeLab.API.Profiles.Domain.Repositories;
using CafeLab.API.Profiles.Domain.Services;
using CafeLab.API.Profiles.Infrastructure.Persistance.EFC.Repositories;
using CafeLab.API.Shared.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.CoffeeProduction.Application.Internal.CommandServices;
using CafeLab.API.CoffeeProduction.Application.Internal.QueryServices;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Infrastructure.Persistance.EFC.Repositories;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

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

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection:
// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// otros bounded contexts...

// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// CoffeeProduction Bounded Context
builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

// CoffeeLot dependencies
builder.Services.AddScoped<ICoffeeLotCommandService, CoffeeLotCommandService>();
builder.Services.AddScoped<ICoffeeLotQueryService, CoffeeLotQueryService>();
builder.Services.AddScoped<ICoffeeLotRepository, CoffeeLotRepository>();
builder.Services.AddScoped<CoffeeLotResourceFromEntityAssembler>();
builder.Services.AddScoped<CreateCoffeeLotCommandFromResourceAssembler>();

// IAM Bounded Context

// TokenSettings Configuration

// RoastProfile dependencies
builder.Services.AddScoped<IRoastProfileCommandService, RoastProfileCommandService>();
builder.Services.AddScoped<IRoastProfileQueryService, RoastProfileQueryService>();
builder.Services.AddScoped<IRoastProfileRepository, RoastProfileRepository>();
builder.Services.AddScoped<RoastProfileResourceFromEntityAssembler>();
builder.Services.AddScoped<CreateRoastProfileCommandFromResourceAssembler>();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();