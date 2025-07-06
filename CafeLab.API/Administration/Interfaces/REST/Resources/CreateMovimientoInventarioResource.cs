using System;

namespace CafeLab.API.Administration.Interfaces.REST.Resources
{
    public class CreateMovimientoInventarioResource
    {
        public DateTime Fecha { get; set; }
        public string Lote { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string TipoCafe { get; set; }
        public int UserId { get; set; }
    }
} 