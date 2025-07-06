using CafeLab.API.Defects.Domain.Model.Commands;
using CafeLab.API.Defects.Domain.Model.Queries;
using CafeLab.API.Defects.Domain.Services;
using CafeLab.API.Defects.Interfaces.REST.Resources;
using CafeLab.API.Defects.Interfaces.REST.Transform;
using CafeLab.API.IAM.Interfaces.ASP.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeLab.API.Defects.Interfaces.REST;

/// <summary>
/// Defects controller for defect management
/// 
/// Este controlador maneja todas las operaciones HTTP relacionadas con defectos
/// del café en el bounded context Defects.
/// 
/// Endpoints disponibles:
/// - GET /: Listar defectos del usuario autenticado
/// - GET /{id}: Obtener un defecto específico
/// - GET /search: Buscar defectos por nombre
/// - POST /: Crear un nuevo defecto
/// - PUT /{id}: Actualizar un defecto existente
/// - DELETE /{id}: Eliminar (desactivar) un defecto
/// 
/// Todos los endpoints requieren autenticación JWT y los defectos
/// están asociados al usuario autenticado.
/// </summary>
[ApiController]
[Route("api/v1/defects")]
[Authorize]
public class DefectsController : ControllerBase
{
    private readonly IDefectCommandService _defectCommandService;
    private readonly IDefectQueryService _defectQueryService;

    public DefectsController(IDefectCommandService defectCommandService, IDefectQueryService defectQueryService)
    {
        _defectCommandService = defectCommandService;
        _defectQueryService = defectQueryService;
    }

    /// <summary>
    /// Get all defects for the current user
    /// </summary>
    /// <returns>List of defects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DefectResource>>> GetAll()
    {
        var userId = GetCurrentUserId();
        var query = new GetAllDefectsByUserIdQuery(userId);
        var defects = await _defectQueryService.GetAllByUserIdAsync(query);
        var resources = defects.Select(d => DefectResourceFromEntityAssembler.ToResourceFromEntity(d));
        return Ok(resources);
    }

    /// <summary>
    /// Get a defect by ID
    /// </summary>
    /// <param name="id">Defect ID</param>
    /// <returns>Defect information</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DefectResource>> GetById(int id)
    {
        var query = new GetDefectByIdQuery(id);
        var defect = await _defectQueryService.GetByIdAsync(query);
        
        if (defect == null)
            return NotFound();

        var resource = DefectResourceFromEntityAssembler.ToResourceFromEntity(defect);
        return Ok(resource);
    }

    /// <summary>
    /// Search defects by name
    /// </summary>
    /// <param name="name">Name to search for</param>
    /// <returns>List of matching defects</returns>
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<DefectResource>>> SearchByName([FromQuery] string name)
    {
        var userId = GetCurrentUserId();
        var query = new SearchDefectsByNameQuery(name, userId);
        var defects = await _defectQueryService.SearchByNameAsync(query);
        var resources = defects.Select(d => DefectResourceFromEntityAssembler.ToResourceFromEntity(d));
        return Ok(resources);
    }

    /// <summary>
    /// Create a new defect
    /// </summary>
    /// <param name="resource">Defect creation data</param>
    /// <returns>Created defect information</returns>
    [HttpPost]
    public async Task<ActionResult<DefectResource>> Create([FromBody] CreateDefectResource resource)
    {
        try
        {
            var userId = GetCurrentUserId();
            var command = CreateDefectCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
            var defect = await _defectCommandService.CreateAsync(command);
            var responseResource = DefectResourceFromEntityAssembler.ToResourceFromEntity(defect);
            return CreatedAtAction(nameof(GetById), new { id = defect.Id }, responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update an existing defect
    /// </summary>
    /// <param name="id">Defect ID</param>
    /// <param name="resource">Updated defect data</param>
    /// <returns>Updated defect information</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<DefectResource>> Update(int id, [FromBody] CreateDefectResource resource)
    {
        try
        {
            var command = new UpdateDefectCommand(id, resource.Name, resource.Description, resource.Category, resource.ProbableCauses, resource.RecommendedSolutions);
            var defect = await _defectCommandService.UpdateAsync(command);
            var responseResource = DefectResourceFromEntityAssembler.ToResourceFromEntity(defect);
            return Ok(responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete a defect
    /// </summary>
    /// <param name="id">Defect ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteDefectCommand(id);
            await _defectCommandService.DeleteAsync(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private int GetCurrentUserId()
    {
        if (HttpContext.Items["UserId"] is int userId)
            return userId;
        
        throw new InvalidOperationException("User ID not found in context");
    }
} 