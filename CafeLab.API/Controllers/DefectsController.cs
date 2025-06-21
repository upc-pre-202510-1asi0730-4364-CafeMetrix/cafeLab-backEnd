using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CafeLab.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DefectsController : ControllerBase
{
    private readonly IRepository<Defect> _defectRepository;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public DefectsController(IRepository<Defect> defectRepository, IStringLocalizer<SharedResource> localizer)
    {
        _defectRepository = defectRepository;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Defect>>> GetDefects()
    {
        var defects = await _defectRepository.GetAllAsync();
        return Ok(new { data = defects });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Defect>> GetDefect(int id)
    {
        var defect = await _defectRepository.GetByIdAsync(id);
        if (defect == null)
        {
            return NotFound(new { message = _localizer["DefectNotFound"] });
        }
        return Ok(new { data = defect });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Defect>> CreateDefect(Defect defect)
    {
        if (string.IsNullOrWhiteSpace(defect.Name))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Description))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Category))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Severity))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Solution))
            return BadRequest(new { message = _localizer["RequiredField"] });

        await _defectRepository.AddAsync(defect);
        return CreatedAtAction(nameof(GetDefect), new { id = defect.Id }, new { message = _localizer["DefectCreated"], data = defect });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDefect(int id, Defect defect)
    {
        if (id != defect.Id)
        {
            return BadRequest(new { message = _localizer["IdMismatch"] });
        }

        var existingDefect = await _defectRepository.GetByIdAsync(id);
        if (existingDefect == null)
        {
            return NotFound(new { message = _localizer["DefectNotFound"] });
        }

        if (string.IsNullOrWhiteSpace(defect.Name))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Description))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Category))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Severity))
            return BadRequest(new { message = _localizer["RequiredField"] });
        if (string.IsNullOrWhiteSpace(defect.Solution))
            return BadRequest(new { message = _localizer["RequiredField"] });

        await _defectRepository.UpdateAsync(defect);
        return Ok(new { message = _localizer["DefectUpdated"], data = defect });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDefect(int id)
    {
        var defect = await _defectRepository.GetByIdAsync(id);
        if (defect == null)
        {
            return NotFound(new { message = _localizer["DefectNotFound"] });
        }
        await _defectRepository.DeleteAsync(defect);
        return Ok(new { message = _localizer["DefectDeleted"] });
    }
} 