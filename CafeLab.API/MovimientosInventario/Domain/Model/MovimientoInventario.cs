using System;

namespace CafeLab.API.MovimientosInventario.Domain.Model;

public class MovimientoInventario
{
    public string Id { get; set; }
    public DateOnly Fecha { get; set; }
    public string Lote { get; set; }
    public string Producto { get; set; }
    public string Cantidad { get; set; }
    public string TipoCafe { get; set; }

    public MovimientoInventario(
        string id, DateOnly fecha, string lote, string producto, string cantidad, string tipoCafe)
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