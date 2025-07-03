namespace CafeLab.API.CoffeeProduction.Domain.Model.Commands;

public class UpdateRoastProfileCommand
{
    public int Id { get; set; }
    public string ProfileName { get; set; } = string.Empty;
    public string RoastType { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int CoffeeLotId { get; set; }
    public int TempStart { get; set; }
    public int TempEnd { get; set; }
    public bool IsFavorite { get; set; } = false;
} 