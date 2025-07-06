using CafeLab.API.Preparation.Domain.Model.Commands;

namespace CafeLab.API.Preparation.Domain.Model.Aggregates;

/// <summary>
/// Portfolio Aggregate Root
/// </summary>
/// <remarks>
/// This class represents the Portfolio aggregate root.
/// It contains the properties and methods to manage portfolio information.
/// </remarks>
public partial class Portfolio
{
    public int Id { get; }
    
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int UserId { get; private set; }
    
    // Navigation property for recipes
    public ICollection<Recipe> Recipes { get; private set; }

    public Portfolio()
    {
        Name = string.Empty;
        Recipes = new List<Recipe>();
    }
    
    public Portfolio(string name, int userId)
    {
        Name = name;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        Recipes = new List<Recipe>();
    }
    
    public Portfolio(CreatePortfolioCommand command)
    {
        Name = command.Name;
        UserId = command.UserId;
        CreatedAt = DateTime.UtcNow;
        Recipes = new List<Recipe>();
    }

    /// <summary>
    /// Updates the portfolio name
    /// </summary>
    public void Update(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Adds a recipe to the portfolio
    /// </summary>
    public void AddRecipe(Recipe recipe)
    {
        if (recipe.UserId != UserId)
        {
            throw new InvalidOperationException("Cannot add recipe from different user to portfolio");
        }
        
        if (!Recipes.Any(r => r.Id == recipe.Id))
        {
            recipe.AssignToPortfolio(Id);
            Recipes.Add(recipe);
        }
    }

    /// <summary>
    /// Removes a recipe from the portfolio
    /// </summary>
    public void RemoveRecipe(Recipe recipe)
    {
        var existingRecipe = Recipes.FirstOrDefault(r => r.Id == recipe.Id);
        if (existingRecipe != null)
        {
            existingRecipe.RemoveFromPortfolio();
            Recipes.Remove(existingRecipe);
        }
    }

    /// <summary>
    /// Checks if the portfolio contains a specific recipe
    /// </summary>
    public bool HasRecipe(int recipeId)
    {
        return Recipes.Any(r => r.Id == recipeId);
    }

    /// <summary>
    /// Gets the count of recipes in the portfolio
    /// </summary>
    public int RecipeCount => Recipes.Count;
} 