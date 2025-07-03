using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST;

[ApiController]
[Route("api/v1/roast-profiles")]
public class RoastProfilesController : ControllerBase
{
    private readonly IRoastProfileCommandService _roastProfileCommandService;
    private readonly IRoastProfileQueryService _roastProfileQueryService;
    private readonly RoastProfileResourceFromEntityAssembler _roastProfileResourceFromEntityAssembler;
    private readonly CreateRoastProfileCommandFromResourceAssembler _createRoastProfileCommandFromResourceAssembler;

    public RoastProfilesController(
        IRoastProfileCommandService roastProfileCommandService,
        IRoastProfileQueryService roastProfileQueryService,
        RoastProfileResourceFromEntityAssembler roastProfileResourceFromEntityAssembler,
        CreateRoastProfileCommandFromResourceAssembler createRoastProfileCommandFromResourceAssembler)
    {
        _roastProfileCommandService = roastProfileCommandService;
        _roastProfileQueryService = roastProfileQueryService;
        _roastProfileResourceFromEntityAssembler = roastProfileResourceFromEntityAssembler;
        _createRoastProfileCommandFromResourceAssembler = createRoastProfileCommandFromResourceAssembler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoastProfileResource>>> GetAll([FromQuery] int userId)
    {
        var query = new GetAllRoastProfilesByUserIdQuery { UserId = userId };
        var roastProfiles = await _roastProfileQueryService.GetAllByUserIdAsync(query);
        var resources = roastProfiles.Select(rp => _roastProfileResourceFromEntityAssembler.ToResourceFromEntity(rp));
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoastProfileResource>> GetById(int id)
    {
        var query = new GetRoastProfileByIdQuery { Id = id };
        var roastProfile = await _roastProfileQueryService.GetByIdAsync(query);
        if (roastProfile == null)
            return NotFound();
        var resource = _roastProfileResourceFromEntityAssembler.ToResourceFromEntity(roastProfile);
        return Ok(resource);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<RoastProfileResource>>> SearchByName([FromQuery] string profileName, [FromQuery] int userId)
    {
        var query = new SearchRoastProfilesByNameQuery { ProfileName = profileName, UserId = userId };
        var roastProfiles = await _roastProfileQueryService.SearchByNameAsync(query);
        var resources = roastProfiles.Select(rp => _roastProfileResourceFromEntityAssembler.ToResourceFromEntity(rp));
        return Ok(resources);
    }

    [HttpPost]
    public async Task<ActionResult<RoastProfileResource>> Create([FromBody] CreateRoastProfileResource resource)
    {
        try
        {
            var command = _createRoastProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
            var roastProfile = await _roastProfileCommandService.CreateAsync(command);
            var responseResource = _roastProfileResourceFromEntityAssembler.ToResourceFromEntity(roastProfile);
            return CreatedAtAction(nameof(GetById), new { id = roastProfile.Id }, responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoastProfileResource>> Update(int id, [FromBody] CreateRoastProfileResource resource)
    {
        try
        {
            var command = new UpdateRoastProfileCommand
            {
                Id = id,
                ProfileName = resource.ProfileName,
                RoastType = resource.RoastType,
                Duration = resource.Duration,
                CoffeeLotId = resource.CoffeeLotId,
                TempStart = resource.TempStart,
                TempEnd = resource.TempEnd,
                IsFavorite = resource.IsFavorite
            };
            var roastProfile = await _roastProfileCommandService.UpdateAsync(command);
            var responseResource = _roastProfileResourceFromEntityAssembler.ToResourceFromEntity(roastProfile);
            return Ok(responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteRoastProfileCommand { Id = id };
            await _roastProfileCommandService.DeleteAsync(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 