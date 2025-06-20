using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CafeLab.API.CostosLote.Domain.Model.ValueObjects;

namespace CafeLab.API.CostosLote.Domain.Model;

[Table("CostosLote")]
public class CostoLote
{
    [Key]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public required string Lote { get; set; }
    public required decimal MateriaPrima { get; set; }
    public required decimal ManoObra { get; set; }
    public required decimal Transporte { get; set; }
    public required decimal Almacenamiento { get; set; }
    public decimal Procesamiento { get; set; }
    public required decimal OtrosCostos { get; set; }
    public required Totales Totales { get; set; }
    public required Detalle Detalle { get; set; }

    public CostoLote(
        int id, DateTime fecha, string lote, decimal materiaPrima, decimal manoObra, 
        decimal transporte, decimal almacenamiento, decimal procesamiento, decimal otrosCostos, 
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