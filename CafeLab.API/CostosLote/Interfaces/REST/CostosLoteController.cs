using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CafeLab.API.CostosLote.Application;
using CafeLab.API.CostosLote.Domain.Model;

namespace CafeLab.API.CostosLote.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class CostosLoteController : ControllerBase
{
    private readonly ICostoLoteApplicationService _costoLoteApplicationService;

    public CostosLoteController(ICostoLoteApplicationService costoLoteApplicationService)
    {
        _costoLoteApplicationService = costoLoteApplicationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostoLote>>> GetAll()
    {
        var costosLote = await _costoLoteApplicationService.GetAllAsync();
        return Ok(costosLote);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostoLote>> GetById(int id)
    {
        var costoLote = await _costoLoteApplicationService.GetByIdAsync(id);
        if (costoLote == null)
        {
            return NotFound();
        }
        return Ok(costoLote);
    }

    [HttpPost]
    public async Task<ActionResult<CostoLote>> Create(CostoLote costoLote)
    {
        await _costoLoteApplicationService.AddAsync(costoLote);
        return CreatedAtAction(nameof(GetById), new { id = costoLote.Id }, costoLote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CostoLote costoLote)
    {
        if (id != costoLote.Id)
        {
            return BadRequest();
        }

        var existingCostoLote = await _costoLoteApplicationService.GetByIdAsync(id);
        if (existingCostoLote == null)
        {
            return NotFound();
        }

        await _costoLoteApplicationService.UpdateAsync(costoLote);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var costoLote = await _costoLoteApplicationService.GetByIdAsync(id);
        if (costoLote == null)
        {
            return NotFound();
        }

        await _costoLoteApplicationService.DeleteAsync(id);
        return NoContent();
    }
} 