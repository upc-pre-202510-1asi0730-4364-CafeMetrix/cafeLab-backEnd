using System.Net.Mime;
using CafeLab.API.Preparation.Domain.Model.Commands;
using CafeLab.API.Preparation.Domain.Model.ValueObjects;
using CafeLab.API.Preparation.Domain.Model.Queries;
using CafeLab.API.Preparation.Domain.Services;
using CafeLab.API.Preparation.Interfaces.REST.Resources;
using CafeLab.API.Preparation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CafeLab.API.Preparation.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Recipe Endpoints.")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeCommandService _recipeCommandService;
    private readonly IRecipeQueryService _recipeQueryService;

    /// <summary>
    /// Gets the user ID from the request headers or authentication context
    /// For now, we'll extract it from the request body/query, but this should be replaced
    /// with proper JWT authentication in the future
    /// </summary>
    private int GetCurrentUserId(HttpContext context)
    {
        // TODO: Replace with proper JWT authentication
        // For now, we'll use a default user ID until authentication is implemented
        return 1; // This should come from JWT token or authentication context
    }

    public RecipesController(
        IRecipeCommandService recipeCommandService,
        IRecipeQueryService recipeQueryService)
    {
        _recipeCommandService = recipeCommandService;
        _recipeQueryService = recipeQueryService;
    }

    /// <summary>
    /// Gets all recipes for the current user
    /// </summary>
    /// <returns>List of recipes</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all recipes for the current user",
        Description = "Gets all recipes for the current user",
        OperationId = "GetAllRecipes"
    )]
    [SwaggerResponse(200, "The recipes were found", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> GetAllRecipes()
    {
        var getAllRecipesQuery = new GetAllRecipesQuery();
        var recipes = await _recipeQueryService.Handle(getAllRecipesQuery);
        var recipeResources = recipes.Select(RecipeResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(recipeResources);
    }

    /// <summary>
    /// Gets recipes without portfolio for the current user
    /// </summary>
    /// <returns>List of recipes without portfolio</returns>
    [HttpGet("without-portfolio")]
    [SwaggerOperation(
        Summary = "Gets recipes without portfolio for the current user",
        Description = "Gets recipes without portfolio for the current user",
        OperationId = "GetRecipesWithoutPortfolio"
    )]
    [SwaggerResponse(200, "The recipes were found", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> GetRecipesWithoutPortfolio()
    {
        // Frontend filters by user, so we return all recipes and let frontend handle filtering
        var getAllRecipesQuery = new GetAllRecipesQuery();
        var recipes = await _recipeQueryService.Handle(getAllRecipesQuery);
        var recipeResources = recipes.Select(RecipeResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(recipeResources);
    }

    /// <summary>
    /// Gets recipes by portfolio ID
    /// </summary>
    /// <param name="portfolioId">Portfolio ID</param>
    /// <returns>List of recipes in the portfolio</returns>
    [HttpGet("by-portfolio/{portfolioId:int}")]
    [SwaggerOperation(
        Summary = "Gets recipes by portfolio ID",
        Description = "Gets recipes by portfolio ID for the current user",
        OperationId = "GetRecipesByPortfolioId"
    )]
    [SwaggerResponse(200, "The recipes were found", typeof(IEnumerable<RecipeResource>))]
    [SwaggerResponse(404, "No recipes found for the portfolio")]
    public async Task<IActionResult> GetRecipesByPortfolioId([FromRoute] int portfolioId)
    {
        // Frontend filters by user, so we return all recipes and let frontend handle filtering
        var getAllRecipesQuery = new GetAllRecipesQuery();
        var recipes = await _recipeQueryService.Handle(getAllRecipesQuery);
        var recipeResources = recipes.Select(RecipeResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(recipeResources);
    }

    /// <summary>
    /// Gets a recipe by ID
    /// </summary>
    /// <param name="recipeId">Recipe ID</param>
    /// <returns>The recipe</returns>
    [HttpGet("{recipeId:int}")]
    [SwaggerOperation(
        Summary = "Gets a recipe by ID",
        Description = "Gets a recipe by ID",
        OperationId = "GetRecipeById"
    )]
    [SwaggerResponse(200, "The recipe was found", typeof(RecipeResource))]
    [SwaggerResponse(404, "The recipe was not found")]
    public async Task<IActionResult> GetRecipeById([FromRoute] int recipeId)
    {
        var getRecipeByIdQuery = new GetRecipeByIdQuery(recipeId);
        var recipe = await _recipeQueryService.Handle(getRecipeByIdQuery);
        if (recipe == null) return NotFound();
        
        // Check if recipe belongs to user (for security)
        // Note: In a real app, this would be handled by authentication middleware
        // if (recipe.UserId != currentUserId) return NotFound();
        
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe);
        return Ok(recipeResource);
    }

    /// <summary>
    /// Creates a new recipe
    /// </summary>
    /// <param name="resource">Recipe creation data</param>
    /// <returns>The created recipe</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new recipe",
        Description = "Creates a new recipe for the current user",
        OperationId = "CreateRecipe"
    )]
    [SwaggerResponse(201, "The recipe was created", typeof(RecipeResource))]
    [SwaggerResponse(400, "The recipe data is invalid")]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeResource resource)
    {
        // Extract userId from the resource (sent by frontend)
        var userId = resource.UserId ?? GetCurrentUserId(HttpContext);
        var createRecipeCommand = CreateRecipeCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var recipe = await _recipeCommandService.Handle(createRecipeCommand);
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe);
        return CreatedAtAction(nameof(GetRecipeById), new { recipeId = recipe.Id }, recipeResource);
    }

    /// <summary>
    /// Updates an existing recipe
    /// </summary>
    /// <param name="recipeId">Recipe ID</param>
    /// <param name="resource">Recipe update data</param>
    /// <returns>The updated recipe</returns>
    [HttpPut("{recipeId:int}")]
    [SwaggerOperation(
        Summary = "Updates an existing recipe",
        Description = "Updates an existing recipe for the current user",
        OperationId = "UpdateRecipe"
    )]
    [SwaggerResponse(200, "The recipe was updated", typeof(RecipeResource))]
    [SwaggerResponse(404, "The recipe was not found")]
    public async Task<IActionResult> UpdateRecipe([FromRoute] int recipeId, [FromBody] CreateRecipeResource resource)
    {
        var ingredients = resource.Ingredients.Select(i => 
            new CreateIngredientCommand(i.Name, i.Amount, i.Unit)).ToList();

        var updateRecipeCommand = new UpdateRecipeCommand(
            recipeId,
            resource.Name,
            resource.ImageUrl,
            ExtractionMethodMapper.FromFrontendValue(resource.ExtractionMethod),
            resource.Ratio,
            resource.CuppingSessionId,
            resource.PortfolioId,
            resource.PreparationTime,
            resource.Steps,
            resource.Tips,
            resource.Cupping,
            resource.GrindSize,
            resource.UserId ?? GetCurrentUserId(HttpContext),
            ingredients
        );

        var recipe = await _recipeCommandService.Handle(updateRecipeCommand);
        if (recipe == null) return NotFound();
        
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe);
        return Ok(recipeResource);
    }

    /// <summary>
    /// Deletes a recipe
    /// </summary>
    /// <param name="recipeId">Recipe ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{recipeId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a recipe",
        Description = "Deletes a recipe for the current user",
        OperationId = "DeleteRecipe"
    )]
    [SwaggerResponse(204, "The recipe was deleted")]
    [SwaggerResponse(404, "The recipe was not found")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] int recipeId)
    {
        // For now, we'll use a default user ID until authentication is implemented
        var currentUserId = GetCurrentUserId(HttpContext);
        var deleteRecipeCommand = new DeleteRecipeCommand(recipeId, currentUserId);
        var result = await _recipeCommandService.Handle(deleteRecipeCommand);
        return result ? NoContent() : NotFound();
    }
} 