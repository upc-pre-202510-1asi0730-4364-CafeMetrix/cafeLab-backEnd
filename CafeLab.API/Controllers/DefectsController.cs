using CafeLab.API.Application.DTOs;
using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CafeLab.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DefectsController : ControllerBase
{
    private readonly IRepository<Defect> _defectRepository;
    private readonly ILogger<DefectsController> _logger;

    public DefectsController(
        IRepository<Defect> defectRepository, 
        ILogger<DefectsController> logger)
    {
        _defectRepository = defectRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<DefectsResponseDto>> GetDefects()
    {
        try
        {
            _logger.LogInformation("Obteniendo lista de defectos");
            
            var defects = await _defectRepository.GetAllAsync();
            var defectDtos = defects.Select(d => new DefectDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Category = d.Category,
                Severity = d.Severity,
                Solution = d.Solution,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt,
                IsActive = d.IsActive
            }).ToList();

            _logger.LogInformation("Se obtuvieron {Count} defectos", defectDtos.Count);

            return Ok(new DefectsResponseDto
            {
                Success = true,
                Message = "Defectos obtenidos exitosamente",
                Data = defectDtos
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener defectos");
            return StatusCode(500, new DefectsResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DefectResponseDto>> GetDefect(int id)
    {
        try
        {
            _logger.LogInformation("Obteniendo defecto con ID: {Id}", id);
            
            var defect = await _defectRepository.GetByIdAsync(id);
            if (defect == null)
            {
                _logger.LogWarning("Defecto con ID {Id} no encontrado", id);
                return NotFound(new DefectResponseDto
                {
                    Success = false,
                    Message = "Defecto no encontrado"
                });
            }

            var defectDto = new DefectDto
            {
                Id = defect.Id,
                Name = defect.Name,
                Description = defect.Description,
                Category = defect.Category,
                Severity = defect.Severity,
                Solution = defect.Solution,
                CreatedAt = defect.CreatedAt,
                UpdatedAt = defect.UpdatedAt,
                IsActive = defect.IsActive
            };

            _logger.LogInformation("Defecto con ID {Id} obtenido exitosamente", id);

            return Ok(new DefectResponseDto
            {
                Success = true,
                Message = "Defecto obtenido exitosamente",
                Data = defectDto
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener defecto con ID: {Id}", id);
            return StatusCode(500, new DefectResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult<DefectResponseDto>> CreateDefect(CreateDefectDto createDefectDto)
    {
        try
        {
            _logger.LogInformation("Creando nuevo defecto: {Name}", createDefectDto.Name);
            
            var defect = new Defect(
                createDefectDto.Name,
                createDefectDto.Description,
                createDefectDto.Category,
                createDefectDto.Severity,
                createDefectDto.Solution
            );

            await _defectRepository.AddAsync(defect);

            var defectDto = new DefectDto
            {
                Id = defect.Id,
                Name = defect.Name,
                Description = defect.Description,
                Category = defect.Category,
                Severity = defect.Severity,
                Solution = defect.Solution,
                CreatedAt = defect.CreatedAt,
                UpdatedAt = defect.UpdatedAt,
                IsActive = defect.IsActive
            };

            _logger.LogInformation("Defecto creado exitosamente con ID: {Id}", defect.Id);

            return CreatedAtAction(nameof(GetDefect), new { id = defect.Id }, new DefectResponseDto
            {
                Success = true,
                Message = "Defecto creado exitosamente",
                Data = defectDto
            });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error de validación al crear defecto: {Message}", ex.Message);
            return BadRequest(new DefectResponseDto
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear defecto");
            return StatusCode(500, new DefectResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DefectResponseDto>> UpdateDefect(int id, UpdateDefectDto updateDefectDto)
    {
        try
        {
            _logger.LogInformation("Actualizando defecto con ID: {Id}", id);

            if (id <= 0)
            {
                return BadRequest(new DefectResponseDto
                {
                    Success = false,
                    Message = "ID inválido"
                });
            }

            var existingDefect = await _defectRepository.GetByIdAsync(id);
            if (existingDefect == null)
            {
                _logger.LogWarning("Defecto con ID {Id} no encontrado para actualizar", id);
                return NotFound(new DefectResponseDto
                {
                    Success = false,
                    Message = "Defecto no encontrado"
                });
            }
            
            existingDefect.Update(
                updateDefectDto.Name,
                updateDefectDto.Description,
                updateDefectDto.Category,
                updateDefectDto.Severity,
                updateDefectDto.Solution
            );

            await _defectRepository.UpdateAsync(existingDefect);

            var defectDto = new DefectDto
            {
                Id = existingDefect.Id,
                Name = existingDefect.Name,
                Description = existingDefect.Description,
                Category = existingDefect.Category,
                Severity = existingDefect.Severity,
                Solution = existingDefect.Solution,
                CreatedAt = existingDefect.CreatedAt,
                UpdatedAt = existingDefect.UpdatedAt,
                IsActive = existingDefect.IsActive
            };

            _logger.LogInformation("Defecto con ID {Id} actualizado exitosamente", id);

            return Ok(new DefectResponseDto
            {
                Success = true,
                Message = "Defecto actualizado exitosamente",
                Data = defectDto
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar defecto con ID: {Id}", id);
            return StatusCode(500, new DefectResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DefectResponseDto>> DeleteDefect(int id)
    {
        try
        {
            _logger.LogInformation("Eliminando defecto con ID: {Id}", id);

            var defect = await _defectRepository.GetByIdAsync(id);
            if (defect == null)
            {
                _logger.LogWarning("Defecto con ID {Id} no encontrado para eliminar", id);
                return NotFound(new DefectResponseDto
                {
                    Success = false,
                    Message = "Defecto no encontrado"
                });
            }

            defect.Deactivate();
            
            await _defectRepository.UpdateAsync(defect);

            _logger.LogInformation("Defecto con ID {Id} eliminado exitosamente", id);

            return Ok(new DefectResponseDto
            {
                Success = true,
                Message = "Defecto eliminado exitosamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar defecto con ID: {Id}", id);
            return StatusCode(500, new DefectResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    [HttpPatch("{id}/activate")]
    public async Task<ActionResult<DefectResponseDto>> ActivateDefect(int id)
    {
        try
        {
            _logger.LogInformation("Activando defecto con ID: {Id}", id);

            var defect = await _defectRepository.GetByIdAsync(id);
            if (defect == null)
            {
                return NotFound(new DefectResponseDto
                {
                    Success = false,
                    Message = "Defecto no encontrado"
                });
            }

            defect.Activate();
            
            await _defectRepository.UpdateAsync(defect);

            _logger.LogInformation("Defecto con ID {Id} activado exitosamente", id);

            return Ok(new DefectResponseDto
            {
                Success = true,
                Message = "Defecto activado exitosamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al activar defecto con ID: {Id}", id);
            return StatusCode(500, new DefectResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }
} 