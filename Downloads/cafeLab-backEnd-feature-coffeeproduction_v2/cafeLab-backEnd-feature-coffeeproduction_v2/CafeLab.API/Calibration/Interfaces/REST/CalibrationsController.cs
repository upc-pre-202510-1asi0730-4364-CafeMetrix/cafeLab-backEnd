using CafeLab.API.Calibration.Domain.Model.Commands;
using CafeLab.API.Calibration.Domain.Model.Queries;
using CafeLab.API.Calibration.Domain.Services;
using CafeLab.API.Calibration.Interfaces.REST.Resources;
using CafeLab.API.Calibration.Interfaces.REST.Transform;
using CafeLab.API.IAM.Interfaces.ASP.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeLab.API.Calibration.Interfaces.REST;

/// <summary>
/// Calibrations controller for calibration management
/// </summary>
[ApiController]
[Route("api/v1/calibrations")]
[Authorize]
public class CalibrationsController : ControllerBase
{
    private readonly ICalibrationCommandService _calibrationCommandService;
    private readonly ICalibrationQueryService _calibrationQueryService;

    public CalibrationsController(ICalibrationCommandService calibrationCommandService, ICalibrationQueryService calibrationQueryService)
    {
        _calibrationCommandService = calibrationCommandService;
        _calibrationQueryService = calibrationQueryService;
    }

    /// <summary>
    /// Get all calibrations for the current user
    /// </summary>
    /// <returns>List of calibrations</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalibrationResource>>> GetAll()
    {
        var userId = GetCurrentUserId();
        var query = new GetAllCalibrationsByUserIdQuery(userId);
        var calibrations = await _calibrationQueryService.GetAllByUserIdAsync(query);
        var resources = calibrations.Select(c => CalibrationResourceFromEntityAssembler.ToResourceFromEntity(c));
        return Ok(resources);
    }

    /// <summary>
    /// Get a calibration by ID
    /// </summary>
    /// <param name="id">Calibration ID</param>
    /// <returns>Calibration information</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CalibrationResource>> GetById(int id)
    {
        var query = new GetCalibrationByIdQuery(id);
        var calibration = await _calibrationQueryService.GetByIdAsync(query);
        
        if (calibration == null)
            return NotFound();

        var resource = CalibrationResourceFromEntityAssembler.ToResourceFromEntity(calibration);
        return Ok(resource);
    }

    /// <summary>
    /// Search calibrations by equipment name
    /// </summary>
    /// <param name="equipmentName">Equipment name to search for</param>
    /// <returns>List of matching calibrations</returns>
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CalibrationResource>>> SearchByEquipmentName([FromQuery] string equipmentName)
    {
        var userId = GetCurrentUserId();
        var query = new SearchCalibrationsByEquipmentNameQuery(equipmentName, userId);
        var calibrations = await _calibrationQueryService.SearchByEquipmentNameAsync(query);
        var resources = calibrations.Select(c => CalibrationResourceFromEntityAssembler.ToResourceFromEntity(c));
        return Ok(resources);
    }

    /// <summary>
    /// Create a new calibration
    /// </summary>
    /// <param name="resource">Calibration creation data</param>
    /// <returns>Created calibration information</returns>
    [HttpPost]
    public async Task<ActionResult<CalibrationResource>> Create([FromBody] CreateCalibrationResource resource)
    {
        try
        {
            var userId = GetCurrentUserId();
            var command = CreateCalibrationCommandFromResourceAssembler.ToCommandFromResource(resource, userId);
            var calibration = await _calibrationCommandService.CreateAsync(command);
            var responseResource = CalibrationResourceFromEntityAssembler.ToResourceFromEntity(calibration);
            return CreatedAtAction(nameof(GetById), new { id = calibration.Id }, responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update an existing calibration
    /// </summary>
    /// <param name="id">Calibration ID</param>
    /// <param name="resource">Updated calibration data</param>
    /// <returns>Updated calibration information</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CalibrationResource>> Update(int id, [FromBody] CreateCalibrationResource resource)
    {
        try
        {
            var command = new UpdateCalibrationCommand(id, resource.EquipmentName, resource.EquipmentType, resource.CalibrationMethod, resource.TargetValue, resource.MeasuredValue, resource.Tolerance, resource.Status, resource.Notes, resource.CalibrationDate, resource.NextCalibrationDate);
            var calibration = await _calibrationCommandService.UpdateAsync(command);
            var responseResource = CalibrationResourceFromEntityAssembler.ToResourceFromEntity(calibration);
            return Ok(responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete a calibration
    /// </summary>
    /// <param name="id">Calibration ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteCalibrationCommand(id);
            await _calibrationCommandService.DeleteAsync(command);
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