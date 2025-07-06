using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CafeLab.API.Administration.Domain.Services;
using CafeLab.API.Administration.Interfaces.REST.Resources;
using CafeLab.API.Administration.Interfaces.REST.Transform;
using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Domain.Model.Queries;

namespace CafeLab.API.Administration.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/costosLote")]
    public class CostosLoteController : ControllerBase
    {
        private readonly ICostoLoteCommandService _commandService;
        private readonly ICostoLoteQueryService _queryService;

        // Simulación de usuario autenticado - en producción esto vendría del token JWT
        private const int MockUserId = 1;

        public CostosLoteController(ICostoLoteCommandService commandService, ICostoLoteQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostoLoteResource>>> GetAll()
        {
            var query = new GetAllCostosLoteByUserIdQuery(MockUserId);
            var costos = await _queryService.Handle(query);
            var resources = new List<CostoLoteResource>();
            foreach (var costo in costos)
                resources.Add(CostoLoteResourceFromEntityAssembler.ToResource(costo));
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostoLoteResource>> GetById(int id)
        {
            var query = new GetCostoLoteByIdQuery(id, MockUserId);
            var costo = await _queryService.Handle(query);
            if (costo == null) return NotFound();
            return Ok(CostoLoteResourceFromEntityAssembler.ToResource(costo));
        }

        [HttpPost]
        public async Task<ActionResult<CostoLoteResource>> Create([FromBody] CreateCostoLoteResource resource)
        {
            var command = CreateCostoLoteCommandFromResourceAssembler.ToCommand(resource);
            var costo = await _commandService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = costo.Id }, CostoLoteResourceFromEntityAssembler.ToResource(costo));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CostoLoteResource>> Update(int id, [FromBody] CreateCostoLoteResource resource)
        {
            var command = UpdateCostoLoteCommandFromResourceAssembler.ToCommand(id, MockUserId, resource);
            var costo = await _commandService.Handle(command);
            if (costo == null) return NotFound();
            return Ok(CostoLoteResourceFromEntityAssembler.ToResource(costo));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = DeleteCostoLoteCommandFromResourceAssembler.ToCommand(id, MockUserId);
            await _commandService.Handle(command);
            return NoContent();
        }

        // Endpoints originales mantenidos para compatibilidad
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CostoLoteResource>>> GetAllByUserId(int userId)
        {
            var query = new GetAllCostosLoteByUserIdQuery(userId);
            var costos = await _queryService.Handle(query);
            var resources = new List<CostoLoteResource>();
            foreach (var costo in costos)
                resources.Add(CostoLoteResourceFromEntityAssembler.ToResource(costo));
            return Ok(resources);
        }

        [HttpGet("{id}/user/{userId}")]
        public async Task<ActionResult<CostoLoteResource>> GetByIdAndUserId(int id, int userId)
        {
            var query = new GetCostoLoteByIdQuery(id, userId);
            var costo = await _queryService.Handle(query);
            if (costo == null) return NotFound();
            return Ok(CostoLoteResourceFromEntityAssembler.ToResource(costo));
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<ActionResult<CostoLoteResource>> UpdateWithUserId(int id, int userId, [FromBody] CreateCostoLoteResource resource)
        {
            var command = UpdateCostoLoteCommandFromResourceAssembler.ToCommand(id, userId, resource);
            var costo = await _commandService.Handle(command);
            if (costo == null) return NotFound();
            return Ok(CostoLoteResourceFromEntityAssembler.ToResource(costo));
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> DeleteWithUserId(int id, int userId)
        {
            var command = DeleteCostoLoteCommandFromResourceAssembler.ToCommand(id, userId);
            await _commandService.Handle(command);
            return NoContent();
        }
    }
} 