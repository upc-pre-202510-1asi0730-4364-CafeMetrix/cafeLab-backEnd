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

// IAM Context
using CafeLab.API.IAM.Domain.Services;
using CafeLab.API.IAM.Application.Internal.CommandServices;
using CafeLab.API.IAM.Application.Internal.QueryServices;
using CafeLab.API.IAM.Application.Internal.Services;
using CafeLab.API.IAM.Domain.Repositories;
using CafeLab.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.IAM.Interfaces.ASP.Authorization;

// Defects Context
using CafeLab.API.Defects.Domain.Services;
using CafeLab.API.Defects.Application.Internal.CommandServices;
using CafeLab.API.Defects.Application.Internal.QueryServices;
using CafeLab.API.Defects.Domain.Repositories;
using CafeLab.API.Defects.Infrastructure.Persistence.EFC.Repositories;

// Calibration Context
using CafeLab.API.Calibration.Domain.Services;
using CafeLab.API.Calibration.Application.Internal.CommandServices;
using CafeLab.API.Calibration.Application.Internal.QueryServices;
using CafeLab.API.Calibration.Domain.Repositories;
using CafeLab.API.Calibration.Infrastructure.Persistence.EFC.Repositories;

//builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
using CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
//===============================================================================================
// CONFIGURACIÓN PRINCIPAL DE LA APLICACIÓN
//===============================================================================================
// Este archivo contiene la configuración completa de la aplicación ASP.NET Core,
// incluyendo la configuración de servicios, middleware y bounded contexts.
//===============================================================================================

var builder = WebApplication.CreateBuilder(args);

//===============================================================================================
// CONFIGURACIÓN DE SERVICIOS HTTP Y ROUTING
//===============================================================================================
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Configuración de CORS para permitir peticiones desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//===============================================================================================
// CONFIGURACIÓN DE BASE DE DATOS
//===============================================================================================
// Configuración de la conexión a la base de datos MySQL
// La cadena de conexión se obtiene desde appsettings.json
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

//===============================================================================================
// CONFIGURACIÓN DE DOCUMENTACIÓN Y EXPLORACIÓN DE API
//===============================================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//===============================================================================================
// CONFIGURACIÓN DE INYECCIÓN DE DEPENDENCIAS
//===============================================================================================
// Registro de servicios compartidos entre todos los bounded contexts
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//===============================================================================================
// REGISTRO DE BOUNDED CONTEXTS
//===============================================================================================
// Cada bounded context registra sus servicios de dominio, aplicación e infraestructura
// siguiendo el patrón de inyección de dependencias de .NET Core

// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// CoffeeProduction Bounded Context
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierCommandService>(provider =>
{
    var supplierRepository = provider.GetRequiredService<ISupplierRepository>();
    var profileRepository = provider.GetRequiredService<IProfileRepository>();
    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    return new SupplierCommandService(supplierRepository, profileRepository, unitOfWork);
});

// CoffeeLot dependencies
builder.Services.AddScoped<ICoffeeLotCommandService>(provider =>
{
    var coffeeLotRepository = provider.GetRequiredService<ICoffeeLotRepository>();
    var supplierRepository = provider.GetRequiredService<ISupplierRepository>();
    var profileRepository = provider.GetRequiredService<IProfileRepository>();
    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    return new CoffeeLotCommandService(coffeeLotRepository, supplierRepository, profileRepository, unitOfWork);
});
builder.Services.AddScoped<ICoffeeLotQueryService, CoffeeLotQueryService>();
builder.Services.AddScoped<ICoffeeLotRepository, CoffeeLotRepository>();
builder.Services.AddScoped<CoffeeLotResourceFromEntityAssembler>();
builder.Services.AddScoped<CreateCoffeeLotCommandFromResourceAssembler>();

//===============================================================================================
// IAM BOUNDED CONTEXT - IDENTITY AND ACCESS MANAGEMENT
//===============================================================================================
// Servicios para autenticación, autorización y gestión de usuarios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Configuración de JWT (los parámetros se leen desde appsettings.json)

// RoastProfile dependencies
builder.Services.AddScoped<IRoastProfileCommandService>(provider =>
{
    var roastProfileRepository = provider.GetRequiredService<IRoastProfileRepository>();
    var coffeeLotRepository = provider.GetRequiredService<ICoffeeLotRepository>();
    var profileRepository = provider.GetRequiredService<IProfileRepository>();
    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    return new RoastProfileCommandService(roastProfileRepository, coffeeLotRepository, profileRepository, unitOfWork);
});
builder.Services.AddScoped<IRoastProfileQueryService, RoastProfileQueryService>();
builder.Services.AddScoped<IRoastProfileRepository, RoastProfileRepository>();
builder.Services.AddScoped<RoastProfileResourceFromEntityAssembler>();
builder.Services.AddScoped<CreateRoastProfileCommandFromResourceAssembler>();

//===============================================================================================
// DEFECTS BOUNDED CONTEXT - GESTIÓN DE DEFECTOS DEL CAFÉ
//===============================================================================================
// Servicios para la gestión de defectos, causas probables y soluciones recomendadas
builder.Services.AddScoped<IDefectRepository, DefectRepository>();
builder.Services.AddScoped<IDefectCommandService, DefectCommandService>();
builder.Services.AddScoped<IDefectQueryService, DefectQueryService>();

//===============================================================================================
// CALIBRATION BOUNDED CONTEXT - GESTIÓN DE CALIBRACIÓN DE EQUIPOS
//===============================================================================================
// Servicios para la gestión de calibración de equipos y control de calidad
builder.Services.AddScoped<ICalibrationRepository, CalibrationRepository>();
builder.Services.AddScoped<ICalibrationCommandService, CalibrationCommandService>();
builder.Services.AddScoped<ICalibrationQueryService, CalibrationQueryService>();

//===============================================================================================
// CONSTRUCCIÓN DE LA APLICACIÓN
//===============================================================================================
var app = builder.Build();

//===============================================================================================
// INICIALIZACIÓN DE BASE DE DATOS
//===============================================================================================
// Verificar si la base de datos existe y crearla si no existe
// En desarrollo, esto crea automáticamente las tablas necesarias
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

//===============================================================================================
// CONFIGURACIÓN DEL PIPELINE HTTP
//===============================================================================================
// Configuración de middleware en el orden correcto para el procesamiento de peticiones

// Configuración de Swagger para documentación de API
app.UseSwagger();
app.UseSwaggerUI();

// Aplicar política CORS para permitir peticiones desde el frontend
app.UseCors("AllowAllPolicy");

// Redirección HTTPS para seguridad
app.UseHttpsRedirection();

//===============================================================================================
// MIDDLEWARE DE AUTENTICACIÓN Y AUTORIZACIÓN
//===============================================================================================
// Middleware personalizado para validación de JWT tokens
app.UseMiddleware<JwtAuthorizationMiddleware>();

// Middleware de autorización estándar de ASP.NET Core
app.UseAuthorization();

app.MapControllers();

app.Run();