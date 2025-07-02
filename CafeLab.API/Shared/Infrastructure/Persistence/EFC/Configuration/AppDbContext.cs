//aqui se llaman aggregates y entities :p
using CafeLab.API.Profiles.Domain.Model.Aggregates;

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

        builder.UseSnakeCaseNamingConvention();
    }
}