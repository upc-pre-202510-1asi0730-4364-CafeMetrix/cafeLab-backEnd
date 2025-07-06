using System;

namespace CafeLab.API.Administration.Domain.Model.Aggregates
{
    public class MovimientoInventario
    {
        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Lote { get; private set; }
        public string Producto { get; private set; }
        public int Cantidad { get; private set; }
        public string TipoCafe { get; private set; }
        public int UserId { get; private set; }

        public MovimientoInventario(DateTime fecha, string lote, string producto, int cantidad, string tipoCafe, int userId)
        {
            Fecha = fecha;
            Lote = lote;
            Producto = producto;
            Cantidad = cantidad;
            TipoCafe = tipoCafe;
            UserId = userId;
        }

        public void Update(DateTime fecha, string lote, string producto, int cantidad, string tipoCafe)
        {
            Fecha = fecha;
            Lote = lote;
            Producto = producto;
            Cantidad = cantidad;
            TipoCafe = tipoCafe;
        }
    }
} 