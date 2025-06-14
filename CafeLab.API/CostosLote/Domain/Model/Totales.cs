namespace CafeLab.API.CostosLote.Domain.Model;

public class Totales
{
    public double TotalLote { get; set; }
    public double CostPerKg { get; set; }
    public double CostPerCup { get; set; }

    public Totales(double totalLote, double costPerKg, double costPerCup)
    {
        TotalLote = totalLote;
        CostPerKg = costPerKg;
        CostPerCup = costPerCup;
    }

    // Constructor sin parámetros para Entity Framework Core
    public Totales() { }
} 