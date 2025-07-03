using CafeLab.API.Application.DTOs;
using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CafeLab.API.Controllers
{
    // Controlador API para el módulo de calibraciones (calibrations)
    // Expone los endpoints para crear, leer, actualizar y eliminar calibraciones.
    // Protegido por autenticación JWT.
    //

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CalibrationsController : ControllerBase
    {
        private readonly IRepository<Calibration> _calibrationRepository;
        private readonly ILogger<CalibrationsController> _logger;

        public CalibrationsController(
            IRepository<Calibration> calibrationRepository, 
            ILogger<CalibrationsController> logger)
        {
            _calibrationRepository = calibrationRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CalibrationsResponseDto>> GetCalibrations()
        {
            try
            {
                _logger.LogInformation("Obteniendo lista de calibraciones");
                
                var calibrations = await _calibrationRepository.GetAllAsync();
                var calibrationDtos = calibrations.Select(c => new CalibrationDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CalibrationDate = c.CalibrationDate,
                    Result = c.Result,
                    Status = c.Status,
                    Type = c.Type,
                    Notes = c.Notes,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsActive = c.IsActive
                }).ToList();

                _logger.LogInformation("Se obtuvieron {Count} calibraciones", calibrationDtos.Count);

                return Ok(new CalibrationsResponseDto
                {
                    Success = true,
                    Message = "Calibraciones obtenidas exitosamente",
                    Data = calibrationDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener calibraciones");
                return StatusCode(500, new CalibrationsResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalibrationResponseDto>> GetCalibration(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo calibración con ID: {Id}", id);
                
                var calibration = await _calibrationRepository.GetByIdAsync(id);
                if (calibration == null)
                {
                    _logger.LogWarning("Calibración con ID {Id} no encontrada", id);
                    return NotFound(new CalibrationResponseDto
                    {
                        Success = false,
                        Message = "Calibración no encontrada"
                    });
                }

                var calibrationDto = new CalibrationDto
                {
                    Id = calibration.Id,
                    Name = calibration.Name,
                    Description = calibration.Description,
                    CalibrationDate = calibration.CalibrationDate,
                    Result = calibration.Result,
                    Status = calibration.Status,
                    Type = calibration.Type,
                    Notes = calibration.Notes,
                    CreatedAt = calibration.CreatedAt,
                    UpdatedAt = calibration.UpdatedAt,
                    IsActive = calibration.IsActive
                };

                _logger.LogInformation("Calibración con ID {Id} obtenida exitosamente", id);

                return Ok(new CalibrationResponseDto
                {
                    Success = true,
                    Message = "Calibración obtenida exitosamente",
                    Data = calibrationDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener calibración con ID: {Id}", id);
                return StatusCode(500, new CalibrationResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CalibrationResponseDto>> CreateCalibration(CreateCalibrationDto createCalibrationDto)
        {
            try
            {
                _logger.LogInformation("Creando nueva calibración: {Name}", createCalibrationDto.Name);
                
                var calibration = new Calibration(
                    createCalibrationDto.Name,
                    createCalibrationDto.Description,
                    createCalibrationDto.CalibrationDate,
                    createCalibrationDto.Result,
                    createCalibrationDto.Type
                );

                if (!string.IsNullOrWhiteSpace(createCalibrationDto.Notes))
                {
                    calibration.Notes = createCalibrationDto.Notes;
                }

                await _calibrationRepository.AddAsync(calibration);

                var calibrationDto = new CalibrationDto
                {
                    Id = calibration.Id,
                    Name = calibration.Name,
                    Description = calibration.Description,
                    CalibrationDate = calibration.CalibrationDate,
                    Result = calibration.Result,
                    Status = calibration.Status,
                    Type = calibration.Type,
                    Notes = calibration.Notes,
                    CreatedAt = calibration.CreatedAt,
                    UpdatedAt = calibration.UpdatedAt,
                    IsActive = calibration.IsActive
                };

                _logger.LogInformation("Calibración creada exitosamente con ID: {Id}", calibration.Id);

                return CreatedAtAction(nameof(GetCalibration), new { id = calibration.Id }, new CalibrationResponseDto
                {
                    Success = true,
                    Message = "Calibración creada exitosamente",
                    Data = calibrationDto
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Error de validación al crear calibración: {Message}", ex.Message);
                return BadRequest(new CalibrationResponseDto
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear calibración");
                return StatusCode(500, new CalibrationResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CalibrationResponseDto>> UpdateCalibration(int id, UpdateCalibrationDto updateCalibrationDto)
        {
            try
            {
                _logger.LogInformation("Actualizando calibración con ID: {Id}", id);

                if (id <= 0)
                {
                    return BadRequest(new CalibrationResponseDto
                    {
                        Success = false,
                        Message = "ID inválido"
                    });
                }

                var existingCalibration = await _calibrationRepository.GetByIdAsync(id);
                if (existingCalibration == null)
                {
                    _logger.LogWarning("Calibración con ID {Id} no encontrada para actualizar", id);
                    return NotFound(new CalibrationResponseDto
                    {
                        Success = false,
                        Message = "Calibración no encontrada"
                    });
                }
                
                existingCalibration.Update(
                    updateCalibrationDto.Name,
                    updateCalibrationDto.Description,
                    updateCalibrationDto.CalibrationDate,
                    updateCalibrationDto.Result,
                    updateCalibrationDto.Type,
                    updateCalibrationDto.Status,
                    updateCalibrationDto.Notes
                );

                await _calibrationRepository.UpdateAsync(existingCalibration);

                var calibrationDto = new CalibrationDto
                {
                    Id = existingCalibration.Id,
                    Name = existingCalibration.Name,
                    Description = existingCalibration.Description,
                    CalibrationDate = existingCalibration.CalibrationDate,
                    Result = existingCalibration.Result,
                    Status = existingCalibration.Status,
                    Type = existingCalibration.Type,
                    Notes = existingCalibration.Notes,
                    CreatedAt = existingCalibration.CreatedAt,
                    UpdatedAt = existingCalibration.UpdatedAt,
                    IsActive = existingCalibration.IsActive
                };

                _logger.LogInformation("Calibración con ID {Id} actualizada exitosamente", id);

                return Ok(new CalibrationResponseDto
                {
                    Success = true,
                    Message = "Calibración actualizada exitosamente",
                    Data = calibrationDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar calibración con ID: {Id}", id);
                return StatusCode(500, new CalibrationResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CalibrationResponseDto>> DeleteCalibration(int id)
        {
            try
            {
                _logger.LogInformation("Eliminando calibración con ID: {Id}", id);

                var calibration = await _calibrationRepository.GetByIdAsync(id);
                if (calibration == null)
                {
                    _logger.LogWarning("Calibración con ID {Id} no encontrada para eliminar", id);
                    return NotFound(new CalibrationResponseDto
                    {
                        Success = false,
                        Message = "Calibración no encontrada"
                    });
                }

                calibration.Deactivate();
                
                await _calibrationRepository.UpdateAsync(calibration);

                _logger.LogInformation("Calibración con ID {Id} eliminada exitosamente", id);

                return Ok(new CalibrationResponseDto
                {
                    Success = true,
                    Message = "Calibración eliminada exitosamente"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar calibración con ID: {Id}", id);
                return StatusCode(500, new CalibrationResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<CalibrationResponseDto>> UpdateCalibrationStatus(int id, [FromBody] string status)
        {
            try
            {
                _logger.LogInformation("Actualizando estado de calibración con ID: {Id} a: {Status}", id, status);

                var calibration = await _calibrationRepository.GetByIdAsync(id);
                if (calibration == null)
                {
                    return NotFound(new CalibrationResponseDto
                    {
                        Success = false,
                        Message = "Calibración no encontrada"
                    });
                }

                calibration.UpdateStatus(status);
                
                await _calibrationRepository.UpdateAsync(calibration);

                _logger.LogInformation("Estado de calibración con ID {Id} actualizado exitosamente", id);

                return Ok(new CalibrationResponseDto
                {
                    Success = true,
                    Message = "Estado actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado de calibración con ID: {Id}", id);
                return StatusCode(500, new CalibrationResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        [HttpPatch("{id}/activate")]
        public async Task<ActionResult<CalibrationResponseDto>> ActivateCalibration(int id)
        {
            try
            {
                _logger.LogInformation("Activando calibración con ID: {Id}", id);

                var calibration = await _calibrationRepository.GetByIdAsync(id);
                if (calibration == null)
                {
                    return NotFound(new CalibrationResponseDto
                    {
                        Success = false,
                        Message = "Calibración no encontrada"
                    });
                }

                calibration.Activate();
                
                await _calibrationRepository.UpdateAsync(calibration);

                _logger.LogInformation("Calibración con ID {Id} activada exitosamente", id);

                return Ok(new CalibrationResponseDto
                {
                    Success = true,
                    Message = "Calibración activada exitosamente"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar calibración con ID: {Id}", id);
                return StatusCode(500, new CalibrationResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }
    }
} 