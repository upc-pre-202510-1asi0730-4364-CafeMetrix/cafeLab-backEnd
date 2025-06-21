using CafeLab.API.Domain.Entities;
using CafeLab.API.Shared.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Defect> Defects { get; set; }
    public DbSet<TastingPattern> TastingPatterns { get; set; }
    public DbSet<Calibration> Calibrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable(EntityTableNames.Users);
        modelBuilder.Entity<Defect>().ToTable(EntityTableNames.Defects);
        modelBuilder.Entity<TastingPattern>().ToTable(EntityTableNames.TastingPatterns);
        modelBuilder.Entity<Calibration>().ToTable(EntityTableNames.Calibrations);
    }
} 