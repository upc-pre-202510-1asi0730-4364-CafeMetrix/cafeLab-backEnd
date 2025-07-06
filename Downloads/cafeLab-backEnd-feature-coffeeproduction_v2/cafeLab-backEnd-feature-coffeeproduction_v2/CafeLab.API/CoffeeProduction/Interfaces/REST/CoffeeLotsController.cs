using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;
using CafeLab.API.CoffeeProduction.Domain.Services;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST;

[ApiController]
[Route("api/v1/coffee-lots")]
public class CoffeeLotsController : ControllerBase
{
    private readonly ICoffeeLotCommandService _coffeeLotCommandService;
    private readonly ICoffeeLotQueryService _coffeeLotQueryService;
    private readonly CoffeeLotResourceFromEntityAssembler _coffeeLotResourceFromEntityAssembler;
    private readonly CreateCoffeeLotCommandFromResourceAssembler _createCoffeeLotCommandFromResourceAssembler;

    // Simulación de usuario autenticado
    private const int MockUserId = 1;

    public CoffeeLotsController(
        ICoffeeLotCommandService coffeeLotCommandService,
        ICoffeeLotQueryService coffeeLotQueryService,
        CoffeeLotResourceFromEntityAssembler coffeeLotResourceFromEntityAssembler,
        CreateCoffeeLotCommandFromResourceAssembler createCoffeeLotCommandFromResourceAssembler)
    {
        _coffeeLotCommandService = coffeeLotCommandService;
        _coffeeLotQueryService = coffeeLotQueryService;
        _coffeeLotResourceFromEntityAssembler = coffeeLotResourceFromEntityAssembler;
        _createCoffeeLotCommandFromResourceAssembler = createCoffeeLotCommandFromResourceAssembler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeLotResource>>> GetAll([FromQuery] int userId)
    {
        var query = new GetAllCoffeeLotsByUserIdQuery { UserId = userId };
        var coffeeLots = await _coffeeLotQueryService.GetAllByUserIdAsync(query);
        var resources = coffeeLots.Select(cl => _coffeeLotResourceFromEntityAssembler.ToResourceFromEntity(cl));
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoffeeLotResource>> GetById(int id)
    {
        var query = new GetCoffeeLotByIdQuery { Id = id };
        var coffeeLot = await _coffeeLotQueryService.GetByIdAsync(query);
        if (coffeeLot == null)
            return NotFound();
        var resource = _coffeeLotResourceFromEntityAssembler.ToResourceFromEntity(coffeeLot);
        return Ok(resource);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CoffeeLotResource>>> SearchByName([FromQuery] string lot_name)
    {
        var query = new SearchCoffeeLotsByNameQuery { LotName = lot_name, UserId = MockUserId };
        var coffeeLots = await _coffeeLotQueryService.SearchByNameAsync(query);
        var resources = coffeeLots.Select(cl => _coffeeLotResourceFromEntityAssembler.ToResourceFromEntity(cl));
        return Ok(resources);
    }

    [HttpPost]
    public async Task<ActionResult<CoffeeLotResource>> Create([FromBody] CreateCoffeeLotResource resource)
    {
        try
        {
            var command = _createCoffeeLotCommandFromResourceAssembler.ToCommandFromResource(resource);
            var coffeeLot = await _coffeeLotCommandService.CreateAsync(command);
            var responseResource = _coffeeLotResourceFromEntityAssembler.ToResourceFromEntity(coffeeLot);
            return CreatedAtAction(nameof(GetById), new { id = coffeeLot.Id }, responseResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CoffeeLotResource>> Update(int id, [FromBody] CreateCoffeeLotResource resource)
    {
        try
        {
            var command = new UpdateCoffeeLotCommand
            {
                Id = id,
                LotName = resource.LotName,
                CoffeeType = resource.CoffeeType,
                ProcessingMethod = resource.ProcessingMethod,
                Altitude = resource.Altitude,
                Weight = resource.Weight,
                Certifications = resource.Certifications,
                Origin = resource.Origin,
                SupplierId = resource.SupplierId,
                Status = resource.Status
            };
            var coffeeLot = await _coffeeLotCommandService.UpdateAsync(command);
            var responseResource = _coffeeLotResourceFromEntityAssembler.ToResourceFromEntity(coffeeLot);
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
            var command = new DeleteCoffeeLotCommand { Id = id };
            await _coffeeLotCommandService.DeleteAsync(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 