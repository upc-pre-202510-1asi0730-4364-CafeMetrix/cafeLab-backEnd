using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CalibrationsController : ControllerBase
    {
        private readonly IRepository<Calibration> _calibrationRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CalibrationsController(IRepository<Calibration> calibrationRepository, IStringLocalizer<SharedResource> localizer)
        {
            _calibrationRepository = calibrationRepository;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calibration>>> GetCalibrations()
        {
            var calibrations = await _calibrationRepository.GetAllAsync();
            return Ok(new { data = calibrations });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calibration>> GetCalibration(int id)
        {
            var calibration = await _calibrationRepository.GetByIdAsync(id);
            if (calibration == null)
            {
                return NotFound(new { message = _localizer["CalibrationNotFound"] });
            }
            return Ok(new { data = calibration });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Calibration>> CreateCalibration(Calibration calibration)
        {
            await _calibrationRepository.AddAsync(calibration);
            return CreatedAtAction(nameof(GetCalibration), new { id = calibration.Id }, new { message = _localizer["CalibrationCreated"], data = calibration });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCalibration(int id, Calibration calibration)
        {
            if (id != calibration.Id)
            {
                return BadRequest(new { message = _localizer["IdMismatch"] });
            }

            var existingCalibration = await _calibrationRepository.GetByIdAsync(id);
            if (existingCalibration == null)
            {
                return NotFound(new { message = _localizer["CalibrationNotFound"] });
            }

            await _calibrationRepository.UpdateAsync(calibration);
            return Ok(new { message = _localizer["CalibrationUpdated"], data = calibration });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCalibration(int id)
        {
            var calibration = await _calibrationRepository.GetByIdAsync(id);
            if (calibration == null)
            {
                return NotFound(new { message = _localizer["CalibrationNotFound"] });
            }
            await _calibrationRepository.DeleteAsync(calibration);
            return Ok(new { message = _localizer["CalibrationDeleted"] });
        }
    }
} 