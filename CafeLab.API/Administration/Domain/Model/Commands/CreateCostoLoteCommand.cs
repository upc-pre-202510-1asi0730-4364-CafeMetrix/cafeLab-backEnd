using System;

namespace CafeLab.API.Administration.Domain.Model.Commands
{
    public class CreateCostoLoteCommand
    {
        public DateTime Fecha { get; }
        public string Lote { get; }
        public decimal MateriaPrima { get; }
        public decimal ManoObra { get; }
        public decimal Transporte { get; }
        public decimal Almacenamiento { get; }
        public decimal Procesamiento { get; }
        public decimal OtrosCostos { get; }
        public int UserId { get; }

        public CreateCostoLoteCommand(DateTime fecha, string lote, decimal materiaPrima, decimal manoObra, decimal transporte, decimal almacenamiento, decimal procesamiento, decimal otrosCostos, int userId)
        {
            Fecha = fecha;
            Lote = lote;
            MateriaPrima = materiaPrima;
            ManoObra = manoObra;
            Transporte = transporte;
            Almacenamiento = almacenamiento;
            Procesamiento = procesamiento;
            OtrosCostos = otrosCostos;
            UserId = userId;
        }
    }
} 