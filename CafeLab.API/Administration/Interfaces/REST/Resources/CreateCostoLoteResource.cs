using System;

namespace CafeLab.API.Administration.Interfaces.REST.Resources
{
    public class CreateCostoLoteResource
    {
        public DateTime Fecha { get; set; }
        public string Lote { get; set; }
        public decimal MateriaPrima { get; set; }
        public decimal ManoObra { get; set; }
        public decimal Transporte { get; set; }
        public decimal Almacenamiento { get; set; }
        public decimal Procesamiento { get; set; }
        public decimal OtrosCostos { get; set; }
        public int UserId { get; set; }
    }
} 