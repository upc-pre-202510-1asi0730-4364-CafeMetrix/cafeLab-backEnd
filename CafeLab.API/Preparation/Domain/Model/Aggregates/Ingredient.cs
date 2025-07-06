using CafeLab.API.Preparation.Domain.Model.ValueObjects;

namespace CafeLab.API.Preparation.Domain.Model.Aggregates;

/// <summary>
/// Ingredient Entity
/// </summary>
/// <remarks>
/// This class represents an ingredient that belongs to a recipe.
/// It's part of the Recipe aggregate.
/// </remarks>
public class Ingredient
{
    public int Id { get; }
    
    public int RecipeId { get; private set; }
    public string Name { get; private set; }
    public double Amount { get; private set; }
    public Unit Unit { get; private set; }
    
    // Navigation property
    public Recipe Recipe { get; private set; }

    public Ingredient()
    {
        Name = string.Empty;
        Unit = new Unit();
    }
    
    public Ingredient(string name, double amount, Unit unit, int recipeId)
    {
        Name = name;
        Amount = amount;
        Unit = unit;
        RecipeId = recipeId;
    }

    /// <summary>
    /// Updates the ingredient information
    /// </summary>
    public void Update(string name, double amount, string unit)
    {
        Name = name;
        Amount = amount;
        Unit = new Unit(unit);
    }

    /// <summary>
    /// Validates the ingredient data
    /// </summary>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) &&
               Amount > 0 &&
               Unit != null &&
               !string.IsNullOrWhiteSpace(Unit.Value) &&
               RecipeId > 0;
    }
} 