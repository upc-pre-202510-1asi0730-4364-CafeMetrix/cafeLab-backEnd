using CafeLab.API.CostosLote.Domain.Model;
using CafeLab.API.CuppingSessions.Domain.Model;
using CafeLab.API.MovimientosInventario.Domain.Model;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;

namespace CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<CostoLote> CostosLote { get; set; }
    public DbSet<CuppingSession> CuppingSessions { get; set; }
    public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CostosLote Context
        modelBuilder.Entity<CostoLote>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Lote).IsRequired().HasMaxLength(50);
            entity.Property(e => e.MateriaPrima).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ManoObra).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Transporte).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Almacenamiento).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Procesamiento).IsRequired();
            entity.Property(e => e.OtrosCostos).IsRequired();
            entity.Property(e => e.Fecha).IsRequired();
            entity.ComplexProperty(e => e.Totales);
            entity.ComplexProperty(e => e.Detalle);
        });

        // CuppingSessions Context
        modelBuilder.Entity<CuppingSession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Origin).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Variety).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Process).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Lot).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Profile).IsRequired().HasMaxLength(40);
            entity.ComplexProperty(e => e.Ratings);
        });

        // MovimientosInventario Context
        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Fecha).IsRequired();
            entity.Property(e => e.Lote).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Producto).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cantidad).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TipoCafe).IsRequired().HasMaxLength(100);
        });

        modelBuilder.UseSnakeCaseNamingConvention();
    }
} 