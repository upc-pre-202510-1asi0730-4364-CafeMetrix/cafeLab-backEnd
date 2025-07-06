namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

public class CreateCoffeeLotResource
{
    public string LotName { get; set; } = string.Empty;
    public string CoffeeType { get; set; } = string.Empty;
    public string ProcessingMethod { get; set; } = string.Empty;
    public int Altitude { get; set; }
    public decimal Weight { get; set; }
    public List<string> Certifications { get; set; } = new();
    public string Origin { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = "Active";
} 