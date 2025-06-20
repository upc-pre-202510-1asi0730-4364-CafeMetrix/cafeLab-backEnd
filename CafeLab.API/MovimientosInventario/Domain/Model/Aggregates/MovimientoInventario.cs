using System;

namespace CafeLab.API.MovimientosInventario.Domain.Model;

public class MovimientoInventario
{
    public required string Id { get; set; }
    public DateTime Fecha { get; set; }
    public required string Lote { get; set; }
    public required string Producto { get; set; }
    public required string Cantidad { get; set; }
    public required string TipoCafe { get; set; }

    public MovimientoInventario(
        string id, DateTime fecha, string lote, string producto, string cantidad, string tipoCafe)
    {
        Id = id;
        Fecha = fecha;
        Lote = lote;
        Producto = producto;
        Cantidad = cantidad;
        TipoCafe = tipoCafe;
    }

    // Constructor sin parámetros para Entity Framework Core
    public MovimientoInventario() { }
} 