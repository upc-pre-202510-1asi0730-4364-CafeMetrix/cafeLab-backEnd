using Microsoft.EntityFrameworkCore;
using CafeLab.API.CuppingSessions.Domain.Model;
using CafeLab.API.CostosLote.Domain.Model;
using CafeLab.API.MovimientosInventario.Domain.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CafeLab.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CuppingSession> CuppingSessions { get; set; }
    public DbSet<CostoLote> CostosLote { get; set; }
    public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CuppingSession>(entity =>
        {
            entity.OwnsOne(cs => cs.Ratings);
        });

        modelBuilder.Entity<CostoLote>(entity =>
        {
            entity.OwnsOne(cl => cl.Totales);
            entity.OwnsOne(cl => cl.Detalle);
        });

        // Configuración global para DateOnly
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateOnly))
                {
                    var converter = new ValueConverter<DateOnly, DateTime>(
                        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                        dateTime => DateOnly.FromDateTime(dateTime));
                    property.SetValueConverter(converter);
                }
            }
        }
    }

    // Aquí agregaremos los DbSet para nuestras entidades
    // Por ejemplo:
    // public DbSet<Producto> Productos { get; set; }
    // public DbSet<Categoria> Categorias { get; set; }
} 