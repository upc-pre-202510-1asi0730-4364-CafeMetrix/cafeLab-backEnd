namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

public class RoastProfileResource
{
    public int Id { get; set; }
    public string ProfileName { get; set; } = string.Empty;
    public string RoastType { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int CoffeeLotId { get; set; }
    public string CoffeeLotName { get; set; } = string.Empty;
    public int TempStart { get; set; }
    public int TempEnd { get; set; }
    public bool IsFavorite { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
} 