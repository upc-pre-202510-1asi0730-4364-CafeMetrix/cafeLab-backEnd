using System;

namespace CafeLab.API.Administration.Domain.Model.Aggregates
{
    public class CostoLote
    {
        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Lote { get; private set; }
        public decimal MateriaPrima { get; private set; }
        public decimal ManoObra { get; private set; }
        public decimal Transporte { get; private set; }
        public decimal Almacenamiento { get; private set; }
        public decimal Procesamiento { get; private set; }
        public decimal OtrosCostos { get; private set; }
        public int UserId { get; private set; }

        public CostoLote(DateTime fecha, string lote, decimal materiaPrima, decimal manoObra, decimal transporte, decimal almacenamiento, decimal procesamiento, decimal otrosCostos, int userId)
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

        public void Update(DateTime fecha, string lote, decimal materiaPrima, decimal manoObra, decimal transporte, decimal almacenamiento, decimal procesamiento, decimal otrosCostos)
        {
            Fecha = fecha;
            Lote = lote;
            MateriaPrima = materiaPrima;
            ManoObra = manoObra;
            Transporte = transporte;
            Almacenamiento = almacenamiento;
            Procesamiento = procesamiento;
            OtrosCostos = otrosCostos;
        }
    }
} 