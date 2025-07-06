using System.ComponentModel.DataAnnotations;

namespace CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;

public class RoastProfile
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string ProfileName { get; set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string RoastType { get; set; } = string.Empty;
    [Required]
    public int Duration { get; set; }
    [Required]
    public int CoffeeLotId { get; set; }
    [Required]
    public int TempStart { get; set; }
    [Required]
    public int TempEnd { get; set; }
    public bool IsFavorite { get; set; } = false;
    [Required]
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    // Navigation property
    public virtual CoffeeLot CoffeeLot { get; set; } = null!;
} 