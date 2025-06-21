using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CafeLab.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TastingPatternsController : ControllerBase
{
    private readonly IRepository<TastingPattern> _tastingPatternRepository;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public TastingPatternsController(IRepository<TastingPattern> tastingPatternRepository, IStringLocalizer<SharedResource> localizer)
    {
        _tastingPatternRepository = tastingPatternRepository;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TastingPattern>>> GetTastingPatterns()
    {
        var patterns = await _tastingPatternRepository.GetAllAsync();
        return Ok(patterns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TastingPattern>> GetTastingPattern(int id)
    {
        var pattern = await _tastingPatternRepository.GetByIdAsync(id);
        if (pattern == null)
        {
            return NotFound(new { message = _localizer["TastingPatternNotFound"] });
        }
        return Ok(pattern);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TastingPattern>> CreateTastingPattern(TastingPattern pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern.Name))
            return BadRequest(new { message = _localizer["RequiredField"] });
        await _tastingPatternRepository.AddAsync(pattern);
        return CreatedAtAction(nameof(GetTastingPattern), new { id = pattern.Id }, new { message = _localizer["TastingPatternCreated"], pattern });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTastingPattern(int id, TastingPattern pattern)
    {
        if (id != pattern.Id)
        {
            return BadRequest(new { message = _localizer["IdMismatch"] });
        }
        await _tastingPatternRepository.UpdateAsync(pattern);
        return Ok(new { message = _localizer["TastingPatternUpdated"] });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTastingPattern(int id)
    {
        var pattern = await _tastingPatternRepository.GetByIdAsync(id);
        if (pattern == null)
        {
            return NotFound(new { message = _localizer["TastingPatternNotFound"] });
        }
        await _tastingPatternRepository.DeleteAsync(pattern);
        return Ok(new { message = _localizer["TastingPatternDeleted"] });
    }
} 