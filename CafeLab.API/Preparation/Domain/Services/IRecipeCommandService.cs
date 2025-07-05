using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Commands;

namespace CafeLab.API.Preparation.Domain.Services;

/// <summary>
/// Recipe command service interface
/// </summary>
public interface IRecipeCommandService
{
    /// <summary>
    /// Creates a new recipe
    /// </summary>
    /// <param name="command">Create recipe command</param>
    /// <returns>Created recipe</returns>
    Task<Recipe> Handle(CreateRecipeCommand command);
    
    /// <summary>
    /// Updates an existing recipe
    /// </summary>
    /// <param name="command">Update recipe command</param>
    /// <returns>Updated recipe</returns>
    Task<Recipe?> Handle(UpdateRecipeCommand command);
    
    /// <summary>
    /// Deletes a recipe
    /// </summary>
    /// <param name="command">Delete recipe command</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> Handle(DeleteRecipeCommand command);
} 