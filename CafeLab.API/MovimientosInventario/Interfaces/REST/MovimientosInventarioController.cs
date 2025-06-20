using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CafeLab.API.MovimientosInventario.Application;
using CafeLab.API.MovimientosInventario.Domain.Model;

namespace CafeLab.API.MovimientosInventario.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class MovimientosInventarioController : ControllerBase
{
    private readonly IMovimientoInventarioApplicationService _movimientoInventarioApplicationService;

    public MovimientosInventarioController(IMovimientoInventarioApplicationService movimientoInventarioApplicationService)
    {
        _movimientoInventarioApplicationService = movimientoInventarioApplicationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetAll()
    {
        var movimientosInventario = await _movimientoInventarioApplicationService.GetAllAsync();
        return Ok(movimientosInventario);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovimientoInventario>> GetById(string id)
    {
        var movimientoInventario = await _movimientoInventarioApplicationService.GetByIdAsync(id);
        if (movimientoInventario == null)
        {
            return NotFound();
        }
        return Ok(movimientoInventario);
    }

    [HttpPost]
    public async Task<ActionResult<MovimientoInventario>> Create(MovimientoInventario movimientoInventario)
    {
        await _movimientoInventarioApplicationService.AddAsync(movimientoInventario);
        return CreatedAtAction(nameof(GetById), new { id = movimientoInventario.Id }, movimientoInventario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, MovimientoInventario movimientoInventario)
    {
        if (id != movimientoInventario.Id)
        {
            return BadRequest();
        }

        var existingMovimientoInventario = await _movimientoInventarioApplicationService.GetByIdAsync(id);
        if (existingMovimientoInventario == null)
        {
            return NotFound();
        }

        await _movimientoInventarioApplicationService.UpdateAsync(movimientoInventario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var movimientoInventario = await _movimientoInventarioApplicationService.GetByIdAsync(id);
        if (movimientoInventario == null)
        {
            return NotFound();
        }

        await _movimientoInventarioApplicationService.DeleteAsync(id);
        return NoContent();
    }
} 