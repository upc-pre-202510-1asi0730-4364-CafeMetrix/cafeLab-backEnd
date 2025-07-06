using System.ComponentModel.DataAnnotations;

namespace CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;

public class CoffeeLot
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string LotName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string CoffeeType { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string ProcessingMethod { get; set; } = string.Empty;
    
    [Required]
    public int Altitude { get; set; }
    
    [Required]
    public decimal Weight { get; set; }
    
    public string Certifications { get; set; } = string.Empty; // JSON array as string
    
    [Required]
    [StringLength(100)]
    public string Origin { get; set; } = string.Empty;
    
    [Required]
    public int SupplierId { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [StringLength(20)]
    public string Status { get; set; } = "Active";
    
    // Navigation property
    public virtual Supplier Supplier { get; set; } = null!;
} 