using System;

namespace CafeLab.API.CostosLote.Domain.Model;

public class CostoLote
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public required string Lote { get; set; }
    public required string MateriaPrima { get; set; }
    public required string ManoObra { get; set; }
    public required string Transporte { get; set; }
    public required string Almacenamiento { get; set; }
    public int Procesamiento { get; set; }
    public int OtrosCostos { get; set; }
    public required Totales Totales { get; set; }
    public required Detalle Detalle { get; set; }

    public CostoLote(
        int id, DateOnly fecha, string lote, string materiaPrima, string manoObra, 
        string transporte, string almacenamiento, int procesamiento, int otrosCostos, 
        Totales totales, Detalle detalle)
    {
        Id = id;
        Fecha = fecha;
        Lote = lote;
        MateriaPrima = materiaPrima;
        ManoObra = manoObra;
        Transporte = transporte;
        Almacenamiento = almacenamiento;
        Procesamiento = procesamiento;
        OtrosCostos = otrosCostos;
        Totales = totales;
        Detalle = detalle;
    }

    // Constructor sin parámetros para Entity Framework Core
    public CostoLote() { }
} 