using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CafeLab.API.CuppingSessions.Application;
using CafeLab.API.CuppingSessions.Domain.Model;

namespace CafeLab.API.CuppingSessions.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class CuppingSessionsController : ControllerBase
{
    private readonly ICuppingSessionApplicationService _cuppingSessionApplicationService;

    public CuppingSessionsController(ICuppingSessionApplicationService cuppingSessionApplicationService)
    {
        _cuppingSessionApplicationService = cuppingSessionApplicationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CuppingSession>>> GetAll()
    {
        var cuppingSessions = await _cuppingSessionApplicationService.GetAllAsync();
        return Ok(cuppingSessions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CuppingSession>> GetById(string id)
    {
        if (!int.TryParse(id, out int idInt))
        {
            return BadRequest("Invalid ID format");
        }
        var cuppingSession = await _cuppingSessionApplicationService.GetByIdAsync(idInt);
        if (cuppingSession == null)
        {
            return NotFound();
        }
        return Ok(cuppingSession);
    }

    [HttpPost]
    public async Task<ActionResult<CuppingSession>> Create(CuppingSession cuppingSession)
    {
        await _cuppingSessionApplicationService.AddAsync(cuppingSession);
        return CreatedAtAction(nameof(GetById), new { id = cuppingSession.Id }, cuppingSession);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CuppingSession cuppingSession)
    {
        if (!int.TryParse(id, out int idInt))
        {
            return BadRequest("Invalid ID format");
        }

        if (idInt != cuppingSession.Id)
        {
            return BadRequest();
        }

        var existingCuppingSession = await _cuppingSessionApplicationService.GetByIdAsync(idInt);
        if (existingCuppingSession == null)
        {
            return NotFound();
        }

        await _cuppingSessionApplicationService.UpdateAsync(cuppingSession);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!int.TryParse(id, out int idInt))
        {
            return BadRequest("Invalid ID format");
        }

        var cuppingSession = await _cuppingSessionApplicationService.GetByIdAsync(idInt);
        if (cuppingSession == null)
        {
            return NotFound();
        }

        await _cuppingSessionApplicationService.DeleteAsync(idInt);
        return NoContent();
    }
} 