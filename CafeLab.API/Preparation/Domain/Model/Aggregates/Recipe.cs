using CafeLab.API.Preparation.Domain.Model.Commands;

using CafeLab.API.Preparation.Domain.Model.ValueObjects;

namespace CafeLab.API.Preparation.Domain.Model.Aggregates;

/// <summary>
/// Recipe Aggregate Root
/// </summary>
/// <remarks>
/// This class represents the Recipe aggregate root.
/// It contains the properties and methods to manage recipe information.
/// </remarks>
public partial class Recipe
{
    public int Id { get; }
    
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public EExtractionMethod ExtractionMethod { get; private set; }
    public Ratio Ratio { get; private set; }
    public int? CuppingSessionId { get; private set; }
    public int? PortfolioId { get; private set; }
    public int PreparationTime { get; private set; }
    public string Steps { get; private set; }
    public string Tips { get; private set; }
    public string Cupping { get; private set; }
    public GrindSize GrindSize { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    // Navigation property for ingredients
    public ICollection<Ingredient> Ingredients { get; private set; }

    public Recipe()
    {
        Name = string.Empty;
        ImageUrl = string.Empty;
        Ratio = new Ratio();
        Steps = string.Empty;
        Tips = string.Empty;
        Cupping = string.Empty;
        GrindSize = new GrindSize();
        Ingredients = new List<Ingredient>();
    }
    
    public Recipe(string name, string imageUrl, EExtractionMethod extractionMethod, string ratio, 
                  int? cuppingSessionId, int? portfolioId, int preparationTime, string steps, 
                  string tips, string cupping, string? grindSize, int userId)
    {
        Name = name;
        ImageUrl = imageUrl;
        ExtractionMethod = extractionMethod;
        Ratio = new Ratio(ratio);
        CuppingSessionId = cuppingSessionId;
        PortfolioId = portfolioId;
        PreparationTime = preparationTime;
        Steps = steps;
        Tips = tips;
        Cupping = cupping;
        GrindSize = new GrindSize(grindSize);
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        Ingredients = new List<Ingredient>();
    }
    
    public Recipe(CreateRecipeCommand command)
    {
        Name = command.Name;
        ImageUrl = command.ImageUrl;
        ExtractionMethod = command.ExtractionMethod;
        Ratio = new Ratio(command.Ratio);
        CuppingSessionId = command.CuppingSessionId;
        PortfolioId = command.PortfolioId;
        PreparationTime = command.PreparationTime;
        Steps = command.Steps;
        Tips = command.Tips;
        Cupping = command.Cupping;
        GrindSize = new GrindSize(command.GrindSize);
        UserId = command.UserId;
        CreatedAt = DateTime.UtcNow;
        Ingredients = new List<Ingredient>();
    }

    /// <summary>
    /// Updates the recipe with new information
    /// </summary>
    public void Update(string name, string imageUrl, EExtractionMethod extractionMethod, string ratio, 
                      int? cuppingSessionId, int? portfolioId, int preparationTime, string steps, 
                      string tips, string cupping, string? grindSize)
    {
        Name = name;
        ImageUrl = imageUrl;
        ExtractionMethod = extractionMethod;
        Ratio = new Ratio(ratio);
        CuppingSessionId = cuppingSessionId;
        PortfolioId = portfolioId;
        PreparationTime = preparationTime;
        Steps = steps;
        Tips = tips;
        Cupping = cupping;
        GrindSize = new GrindSize(grindSize);
    }

    /// <summary>
    /// Assigns the recipe to a portfolio
    /// </summary>
    public void AssignToPortfolio(int portfolioId)
    {
        PortfolioId = portfolioId;
    }

    /// <summary>
    /// Removes the recipe from any portfolio
    /// </summary>
    public void RemoveFromPortfolio()
    {
        PortfolioId = null;
    }

    /// <summary>
    /// Checks if the recipe is assigned to a portfolio
    /// </summary>
    public bool IsInPortfolio()
    {
        return PortfolioId.HasValue;
    }

    /// <summary>
    /// Adds an ingredient to the recipe
    /// </summary>
    public void AddIngredient(string name, double amount, string unit)
    {
        var ingredient = new Ingredient(name, amount, new Unit(unit), Id);
        Ingredients.Add(ingredient);
    }

    /// <summary>
    /// Removes an ingredient from the recipe
    /// </summary>
    public void RemoveIngredient(int ingredientId)
    {
        var ingredient = Ingredients.FirstOrDefault(i => i.Id == ingredientId);
        if (ingredient != null)
        {
            Ingredients.Remove(ingredient);
        }
    }
} 