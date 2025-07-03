using System.Globalization;
using System.Text;
using CafeLab.API.Application.Interfaces;
using CafeLab.API.Application.Services;
using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Infrastructure.Data;
using CafeLab.API.Infrastructure.Persistence.EFC;
using CafeLab.API.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

/**
 * Punto de entrada principal de la aplicación .NET para el sistema de defectos y calibraciones.
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
 * - Base de datos SQL Server con Entity Framework
 * - Documentación automática con Swagger
 * - Configuración de seguridad y CORS
 * - Logging estructurado
 * 
 * Estructura de la aplicación:
 * - Application: Lógica de negocio y servicios
 * - Domain: Entidades y interfaces del dominio
 * - Infrastructure: Implementaciones de persistencia y seguridad
 * - Controllers: Endpoints de la API REST
 */

var builder = WebApplication.CreateBuilder(args);

/**
 * Configuración de servicios de la aplicación
 * 
 * Se registran todos los servicios necesarios para el funcionamiento
 * de la aplicación, incluyendo servicios de negocio, persistencia,
 * autenticación y documentación.
 */

// Add services to the container.
builder.Services.AddControllers();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDefectRepository, DefectRepository>();
builder.Services.AddScoped<ICalibrationRepository, CalibrationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

// Add localization
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

// Swagger
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

// Authentication
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();