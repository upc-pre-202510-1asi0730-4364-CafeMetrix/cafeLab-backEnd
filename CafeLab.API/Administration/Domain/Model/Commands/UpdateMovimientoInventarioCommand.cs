using System;

namespace CafeLab.API.Administration.Domain.Model.Commands
{
    public class UpdateMovimientoInventarioCommand
    {
        public int Id { get; }
        public DateTime Fecha { get; }
        public string Lote { get; }
        public string Producto { get; }
        public int Cantidad { get; }
        public string TipoCafe { get; }
        public int UserId { get; }

        public UpdateMovimientoInventarioCommand(int id, DateTime fecha, string lote, string producto, int cantidad, string tipoCafe, int userId)
        {
            Id = id;
            Fecha = fecha;
            Lote = lote;
            Producto = producto;
            Cantidad = cantidad;
            TipoCafe = tipoCafe;
            UserId = userId;
        }
    }
} 