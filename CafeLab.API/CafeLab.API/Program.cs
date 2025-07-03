using System.Globalization;
using System.Text;
using CafeLab.API.Application.Interfaces;
using CafeLab.API.Application.Services;
using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Infrastructure.Data;
using CafeLab.API.Infrastructure.Persistence.EFC;
using CafeLab.API.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.Infrastructure.Security;
using CafeLab.API.Profiles.Application.Internal.CommandServices;
using CafeLab.API.Profiles.Application.Internal.QueryServices;
using CafeLab.API.Profiles.Domain.Repositories;
using CafeLab.API.Profiles.Domain.Services;
using CafeLab.API.Profiles.Infrastructure.Persistance.EFC.Repositories;
using CafeLab.API.Shared.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

/**
 * Punto de entrada principal de la aplicación .NET para el sistema CafeLab.
 * 
 * Este archivo configura y inicializa la aplicación ASP.NET Core con:
 * - Configuración de servicios y dependencias
 * - Configuración de autenticación JWT
 * - Configuración de base de datos Entity Framework
 * - Configuración de Swagger/OpenAPI
 * - Configuración de CORS
 * - Configuración de logging y monitoreo
 * 
 * Características implementadas:
 * - Arquitectura limpia con separación de capas
 * - Inyección de dependencias
 * - Autenticación JWT con refresh tokens
 * - Base de datos MySQL con Entity Framework
 * - Documentación automática con Swagger
 * - Configuración de seguridad y CORS
 * - Logging estructurado
 * - Sistema de perfiles y usuarios
 * - Gestión de defectos y calibraciones
 * 
 * Estructura de la aplicación:
 * - Application: Lógica de negocio y servicios
 * - Domain: Entidades y interfaces del dominio
 * - Infrastructure: Implementaciones de persistencia y seguridad
 * - Controllers: Endpoints de la API REST
 * - Profiles: Sistema de perfiles de usuario
 */

var builder = WebApplication.CreateBuilder(args);

/**
 * Configuración de servicios de la aplicación
 * 
 * Se registran todos los servicios necesarios para el funcionamiento
 * de la aplicación, incluyendo servicios de negocio, persistencia,
 * autenticación y documentación.
 */

// =====================
// Configuración de servicios generales
// =====================
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// =====================
// Configuración de CORS unificada
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// =====================
// Configuración de base de datos para ambos contextos
// =====================
// Se obtiene la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

// Contexto compartido (perfiles, etc)
// Se configura para usar MySQL y que las migraciones se apliquen automáticamente
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Contexto de defects/calibration/IAM
// También se configura para usar MySQL y migrar automáticamente
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// =====================
// Swagger
// =====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CafeLab API", Version = "v1" });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// =====================
// Inyección de dependencias
// =====================
// Shared Bounded Context (Perfiles)
builder.Services.AddScoped<CafeLab.API.Shared.Domain.Repositories.IUnitOfWork, CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork>();
builder.Services.AddScoped(typeof(CafeLab.API.Shared.Domain.Repositories.IBaseRepository<>), typeof(CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories.BaseRepository<>));

// Contexto principal (Defects, Calibration, IAM)
builder.Services.AddScoped<CafeLab.API.Domain.Interfaces.IUnitOfWork, CafeLab.API.Infrastructure.Persistence.EFC.UnitOfWork>();
builder.Services.AddScoped(typeof(CafeLab.API.Domain.Interfaces.IRepository<>), typeof(CafeLab.API.Infrastructure.Persistence.EFC.Repositories.BaseRepository<>));

// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// IAM, Defects y Calibration
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDefectRepository, DefectRepository>();
builder.Services.AddScoped<ICalibrationRepository, CalibrationRepository>();

// =====================
// Localización
// =====================
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("es"),
        new CultureInfo("en")
    };
    options.DefaultRequestCulture = new RequestCulture("es");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddHttpContextAccessor();

// =====================
// Autenticación JWT
// =====================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// =====================
// Migración automática de ambos contextos
// =====================
using (var scope = app.Services.CreateScope())
{
    // Migrar contexto de defects/calibration/IAM
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    // Migrar contexto de perfiles (si usas migraciones en AppDbContext)
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // O context.Database.Migrate() si tienes migraciones
}

// =====================
// Middlewares y pipeline
// =====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS (elige la política según el frontend que uses)
app.UseCors("AllowFrontend");
// app.UseCors("AllowAllPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();