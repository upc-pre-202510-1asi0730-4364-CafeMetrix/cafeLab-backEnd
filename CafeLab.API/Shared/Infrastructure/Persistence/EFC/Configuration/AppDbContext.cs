//aqui se llaman aggregates y entities :p
using CafeLab.API.Profiles.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.ValueObjects;

//ESTO ES OBLIGATORIO -------------------------------------------------------------
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
//---------------------------------------------------------------------------------

/// <summary>
///     Application database context
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

        // Preparation Context - Portfolio
        builder.Entity<Portfolio>().HasKey(p => p.Id);
        builder.Entity<Portfolio>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Portfolio>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Portfolio>().Property(p => p.CreatedAt).IsRequired();
        builder.Entity<Portfolio>().Property(p => p.UserId).IsRequired();

        // Preparation Context - Recipe
        builder.Entity<Recipe>().HasKey(r => r.Id);
        builder.Entity<Recipe>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Recipe>().Property(r => r.UserId).IsRequired();
        builder.Entity<Recipe>().Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Recipe>().Property(r => r.ImageUrl).HasColumnType("TEXT");
        builder.Entity<Recipe>().Property(r => r.ExtractionMethod).IsRequired();
        builder.Entity<Recipe>().Property(r => r.Ratio)
            .HasMaxLength(10)
            .HasConversion(
                v => v.Value,
                v => new Ratio(v));
        builder.Entity<Recipe>().Property(r => r.CuppingSessionId);
        builder.Entity<Recipe>().Property(r => r.PortfolioId);
        builder.Entity<Recipe>().Property(r => r.PreparationTime).IsRequired();
        builder.Entity<Recipe>().Property(r => r.Steps).HasColumnType("TEXT");
        builder.Entity<Recipe>().Property(r => r.Tips).HasColumnType("TEXT");
        builder.Entity<Recipe>().Property(r => r.Cupping).HasMaxLength(200);
        builder.Entity<Recipe>().Property(r => r.GrindSize)
            .HasMaxLength(20)
            .HasConversion(
                v => v.Value,
                v => new GrindSize(v));
        builder.Entity<Recipe>().Property(r => r.CreatedAt).IsRequired();

        // Preparation Context - Ingredient
        builder.Entity<Ingredient>().HasKey(i => i.Id);
        builder.Entity<Ingredient>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Ingredient>().Property(i => i.RecipeId).IsRequired();
        builder.Entity<Ingredient>().Property(i => i.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Ingredient>().Property(i => i.Amount).IsRequired().HasPrecision(10, 2);
        builder.Entity<Ingredient>().Property(i => i.Unit)
            .IsRequired()
            .HasMaxLength(10)
            .HasConversion(
                v => v.Value,
                v => new Unit(v));

        // Relationships for Preparation Context
        builder.Entity<Recipe>()
            .HasMany(r => r.Ingredients)
            .WithOne(i => i.Recipe)
            .HasForeignKey(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.UseSnakeCaseNamingConvention();
    }

    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<CoffeeLot> CoffeeLots { get; set; }
    public DbSet<RoastProfile> RoastProfiles { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
}