using System;

namespace CafeLab.API.Administration.Domain.Model.Commands
{
    public class CreateMovimientoInventarioCommand
    {
        public DateTime Fecha { get; }
        public string Lote { get; }
        public string Producto { get; }
        public int Cantidad { get; }
        public string TipoCafe { get; }
        public int UserId { get; }

        public CreateMovimientoInventarioCommand(DateTime fecha, string lote, string producto, int cantidad, string tipoCafe, int userId)
        {
            Fecha = fecha;
            Lote = lote;
            Producto = producto;
            Cantidad = cantidad;
            TipoCafe = tipoCafe;
            UserId = userId;
        }
    }
} 