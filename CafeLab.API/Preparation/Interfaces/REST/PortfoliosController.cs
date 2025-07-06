using System.Net.Mime;
using CafeLab.API.Preparation.Domain.Model.Commands;
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
[SwaggerTag("Available Portfolio Endpoints.")]
public class PortfoliosController : ControllerBase
{
    private readonly IPortfolioCommandService _portfolioCommandService;
    private readonly IPortfolioQueryService _portfolioQueryService;

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

    public PortfoliosController(
        IPortfolioCommandService portfolioCommandService,
        IPortfolioQueryService portfolioQueryService)
    {
        _portfolioCommandService = portfolioCommandService;
        _portfolioQueryService = portfolioQueryService;
    }

    /// <summary>
    /// Gets all portfolios for the current user
    /// </summary>
    /// <returns>List of portfolios</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all portfolios for the current user",
        Description = "Gets all portfolios for the current user",
        OperationId = "GetAllPortfolios"
    )]
    [SwaggerResponse(200, "The portfolios were found", typeof(IEnumerable<PortfolioResource>))]
    public async Task<IActionResult> GetAllPortfolios()
    {
        var getAllPortfoliosQuery = new GetAllPortfoliosQuery();
        var portfolios = await _portfolioQueryService.Handle(getAllPortfoliosQuery);
        var portfolioResources = portfolios.Select(PortfolioResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(portfolioResources);
    }

    /// <summary>
    /// Gets a portfolio by ID
    /// </summary>
    /// <param name="portfolioId">Portfolio ID</param>
    /// <returns>The portfolio with its recipes</returns>
    [HttpGet("{portfolioId:int}")]
    [SwaggerOperation(
        Summary = "Gets a portfolio by ID",
        Description = "Gets a portfolio by ID with its recipes",
        OperationId = "GetPortfolioById"
    )]
    [SwaggerResponse(200, "The portfolio was found", typeof(PortfolioResource))]
    [SwaggerResponse(404, "The portfolio was not found")]
    public async Task<IActionResult> GetPortfolioById([FromRoute] int portfolioId)
    {
        var getPortfolioByIdQuery = new GetPortfolioByIdQuery(portfolioId);
        var portfolio = await _portfolioQueryService.Handle(getPortfolioByIdQuery);
        if (portfolio == null) return NotFound();
        
        // Check if portfolio belongs to user (for security)
        // Note: In a real app, this would be handled by authentication middleware
        // if (portfolio.UserId != currentUserId) return NotFound();
        
        var portfolioResource = PortfolioResourceFromEntityAssembler.ToResourceFromEntity(portfolio);
        return Ok(portfolioResource);
    }

    /// <summary>
    /// Creates a new portfolio
    /// </summary>
    /// <param name="resource">Portfolio creation data</param>
    /// <returns>The created portfolio</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new portfolio",
        Description = "Creates a new portfolio for the current user",
        OperationId = "CreatePortfolio"
    )]
    [SwaggerResponse(201, "The portfolio was created", typeof(PortfolioResource))]
    [SwaggerResponse(400, "The portfolio data is invalid")]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioResource resource)
    {
        // Extract userId from the resource (sent by frontend)
        var userId = resource.UserId ?? GetCurrentUserId(HttpContext);
        var createPortfolioCommand = CreatePortfolioCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
        var portfolio = await _portfolioCommandService.Handle(createPortfolioCommand);
        var portfolioResource = PortfolioResourceFromEntityAssembler.ToResourceFromEntity(portfolio);
        return CreatedAtAction(nameof(GetPortfolioById), new { portfolioId = portfolio.Id }, portfolioResource);
    }

    /// <summary>
    /// Updates an existing portfolio
    /// </summary>
    /// <param name="portfolioId">Portfolio ID</param>
    /// <param name="resource">Portfolio update data</param>
    /// <returns>The updated portfolio</returns>
    [HttpPut("{portfolioId:int}")]
    [SwaggerOperation(
        Summary = "Updates an existing portfolio",
        Description = "Updates an existing portfolio for the current user",
        OperationId = "UpdatePortfolio"
    )]
    [SwaggerResponse(200, "The portfolio was updated", typeof(PortfolioResource))]
    [SwaggerResponse(404, "The portfolio was not found")]
    public async Task<IActionResult> UpdatePortfolio([FromRoute] int portfolioId, [FromBody] CreatePortfolioResource resource)
    {
        // Extract userId from the resource (sent by frontend)
        var userId = resource.UserId ?? GetCurrentUserId(HttpContext);
        var updatePortfolioCommand = new UpdatePortfolioCommand(portfolioId, resource.Name, userId);
        var portfolio = await _portfolioCommandService.Handle(updatePortfolioCommand);
        if (portfolio == null) return NotFound();
        
        var portfolioResource = PortfolioResourceFromEntityAssembler.ToResourceFromEntity(portfolio);
        return Ok(portfolioResource);
    }

    /// <summary>
    /// Deletes a portfolio
    /// </summary>
    /// <param name="portfolioId">Portfolio ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{portfolioId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a portfolio",
        Description = "Deletes a portfolio for the current user",
        OperationId = "DeletePortfolio"
    )]
    [SwaggerResponse(204, "The portfolio was deleted")]
    [SwaggerResponse(404, "The portfolio was not found")]
    public async Task<IActionResult> DeletePortfolio([FromRoute] int portfolioId)
    {
        // For now, we'll use a default user ID until authentication is implemented
        var currentUserId = GetCurrentUserId(HttpContext);
        Console.WriteLine($"DELETE request received for portfolio ID: {portfolioId}, using userId: {currentUserId}");
        var deletePortfolioCommand = new DeletePortfolioCommand(portfolioId, currentUserId);
        var result = await _portfolioCommandService.Handle(deletePortfolioCommand);
        Console.WriteLine($"Delete operation result: {result}");
        return result ? NoContent() : NotFound();
    }
} 