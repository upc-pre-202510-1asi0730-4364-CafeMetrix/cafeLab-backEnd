// Importaciones de aggregates y entities de todos los bounded contexts
// Cada bounded context tiene sus propias entidades que se mapean a la base de datos
using CafeLab.API.Profiles.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.Defects.Domain.Model.Aggregates;
using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;

//ESTO ES OBLIGATORIO -------------------------------------------------------------
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
//---------------------------------------------------------------------------------

/// <summary>
/// Application database context
/// 
/// Este es el contexto principal de Entity Framework que maneja todas las entidades
/// de los diferentes bounded contexts en la aplicación.
/// 
/// Responsabilidades:
/// - Configurar el mapeo de entidades a tablas de base de datos
/// - Definir relaciones entre entidades
/// - Configurar restricciones y validaciones a nivel de base de datos
/// - Manejar la configuración de Value Objects
/// - Aplicar convenciones de nomenclatura
/// 
/// El contexto utiliza el patrón de configuración fluida de Entity Framework
/// para mantener la separación entre el dominio y la infraestructura.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     On configuring the database context
    /// </summary>
    /// <remarks>
    ///     This method is used to configure the database context.
    ///     It also adds the created and updated date interceptor to the database context.
    /// </remarks>
    /// <param name="builder">
    ///     The option builder for the database context
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Profile Context
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(p => p.Name).IsRequired().HasMaxLength(40);
        builder.Entity<Profile>().Property(p => p.Password).IsRequired().HasMaxLength(40);
        builder.Entity<Profile>().Property(p => p.Role).IsRequired().HasMaxLength(16);
        builder.Entity<Profile>().Property(p => p.CafeteriaName).IsRequired().HasMaxLength(30);
        builder.Entity<Profile>().Property(p => p.Experience).IsRequired().HasMaxLength(10);
        builder.Entity<Profile>().Property(p => p.ProfilePicture).IsRequired();
        builder.Entity<Profile>().Property(p => p.PaymentMethod).IsRequired().HasMaxLength(15);
        builder.Entity<Profile>().Property(p => p.IsFirstLogin).IsRequired();
        builder.Entity<Profile>().Property(p => p.Plan).IsRequired().HasMaxLength(10);
        builder.Entity<Profile>().Property(p => p.HasPlan).IsRequired();

        // Mapeo del value object EmailAddress
        builder.Entity<Profile>().OwnsOne(p => p.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(a => a.Address).HasColumnName("EmailAddress").IsRequired().HasMaxLength(40);
            });

        // CoffeeProduction Context - Supplier
        builder.Entity<Supplier>().HasKey(s => s.Id);
        builder.Entity<Supplier>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Supplier>().Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(s => s.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Supplier>().Property(s => s.Phone).HasMaxLength(20);
        builder.Entity<Supplier>().Property(s => s.Location).HasMaxLength(100);
        builder.Entity<Supplier>().Property(s => s.UserId).IsRequired();
        builder.Entity<Supplier>().Property(s => s.Specialties).HasConversion(
            v => string.Join(",", v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        );

        // CoffeeProduction Context - CoffeeLot
        builder.Entity<CoffeeLot>().HasKey(cl => cl.Id);
        builder.Entity<CoffeeLot>().Property(cl => cl.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CoffeeLot>().Property(cl => cl.LotName).IsRequired().HasMaxLength(100);
        builder.Entity<CoffeeLot>().Property(cl => cl.CoffeeType).IsRequired().HasMaxLength(50);
        builder.Entity<CoffeeLot>().Property(cl => cl.ProcessingMethod).IsRequired().HasMaxLength(50);
        builder.Entity<CoffeeLot>().Property(cl => cl.Altitude).IsRequired();
        builder.Entity<CoffeeLot>().Property(cl => cl.Weight).IsRequired().HasPrecision(10, 2);
        builder.Entity<CoffeeLot>().Property(cl => cl.Certifications).HasMaxLength(500);
        builder.Entity<CoffeeLot>().Property(cl => cl.Origin).IsRequired().HasMaxLength(100);
        builder.Entity<CoffeeLot>().Property(cl => cl.SupplierId).IsRequired();
        builder.Entity<CoffeeLot>().Property(cl => cl.UserId).IsRequired();
        builder.Entity<CoffeeLot>().Property(cl => cl.Status).HasMaxLength(20);

        // Relationship: CoffeeLot -> Supplier
        builder.Entity<CoffeeLot>()
            .HasOne(cl => cl.Supplier)
            .WithMany()
            .HasForeignKey(cl => cl.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // CoffeeProduction Context - RoastProfile
        builder.Entity<RoastProfile>().HasKey(rp => rp.Id);
        builder.Entity<RoastProfile>().Property(rp => rp.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<RoastProfile>().Property(rp => rp.ProfileName).IsRequired().HasMaxLength(100);
        builder.Entity<RoastProfile>().Property(rp => rp.RoastType).IsRequired().HasMaxLength(50);
        builder.Entity<RoastProfile>().Property(rp => rp.Duration).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.CoffeeLotId).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.TempStart).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.TempEnd).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.IsFavorite).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.UserId).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.CreatedAt).IsRequired();
        builder.Entity<RoastProfile>().Property(rp => rp.UpdatedAt).IsRequired();
        builder.Entity<RoastProfile>()
            .HasOne(rp => rp.CoffeeLot)
            .WithMany()
            .HasForeignKey(rp => rp.CoffeeLotId)
            .OnDelete(DeleteBehavior.Restrict);

        // IAM Context - User
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
        builder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(20);
        builder.Entity<User>().Property(u => u.IsActive).IsRequired();
        builder.Entity<User>().Property(u => u.LastLoginAt).IsRequired();

        // Mapeo del value object EmailAddress para User
        builder.Entity<User>().OwnsOne(u => u.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(a => a.Address).HasColumnName("EmailAddress").IsRequired().HasMaxLength(100);
            });

        // Defects Context - Defect
        builder.Entity<Defect>().HasKey(d => d.Id);
        builder.Entity<Defect>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Defect>().Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Defect>().Property(d => d.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Defect>().Property(d => d.Category).IsRequired().HasMaxLength(50);
        builder.Entity<Defect>().Property(d => d.ProbableCauses).IsRequired().HasMaxLength(1000);
        builder.Entity<Defect>().Property(d => d.RecommendedSolutions).IsRequired().HasMaxLength(1000);
        builder.Entity<Defect>().Property(d => d.UserId).IsRequired();
        builder.Entity<Defect>().Property(d => d.IsActive).IsRequired();

        // Calibration Context - Calibration
        builder.Entity<CalibrationEntity>().HasKey(c => c.Id);
        builder.Entity<CalibrationEntity>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CalibrationEntity>().Property(c => c.EquipmentName).IsRequired().HasMaxLength(100);
        builder.Entity<CalibrationEntity>().Property(c => c.EquipmentType).IsRequired().HasMaxLength(50);
        builder.Entity<CalibrationEntity>().Property(c => c.CalibrationMethod).IsRequired().HasMaxLength(100);
        builder.Entity<CalibrationEntity>().Property(c => c.TargetValue).IsRequired().HasPrecision(10, 4);
        builder.Entity<CalibrationEntity>().Property(c => c.MeasuredValue).IsRequired().HasPrecision(10, 4);
        builder.Entity<CalibrationEntity>().Property(c => c.Tolerance).IsRequired().HasPrecision(10, 4);
        builder.Entity<CalibrationEntity>().Property(c => c.Status).IsRequired().HasMaxLength(20);
        builder.Entity<CalibrationEntity>().Property(c => c.Notes).HasMaxLength(1000);
        builder.Entity<CalibrationEntity>().Property(c => c.CalibrationDate).IsRequired();
        builder.Entity<CalibrationEntity>().Property(c => c.NextCalibrationDate).IsRequired();
        builder.Entity<CalibrationEntity>().Property(c => c.UserId).IsRequired();
        builder.Entity<CalibrationEntity>().Property(c => c.IsActive).IsRequired();

        builder.UseSnakeCaseNamingConvention();
    }

    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<CoffeeLot> CoffeeLots { get; set; }
    public DbSet<RoastProfile> RoastProfiles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Defect> Defects { get; set; }
    public DbSet<CalibrationEntity> Calibrations { get; set; }
}