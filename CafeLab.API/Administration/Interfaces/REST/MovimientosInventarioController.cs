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
    [Route("api/v1/movimientosInventario")]
    public class MovimientosInventarioController : ControllerBase
    {
        private readonly IMovimientoInventarioCommandService _commandService;
        private readonly IMovimientoInventarioQueryService _queryService;

        // Simulación de usuario autenticado - en producción esto vendría del token JWT
        private const int MockUserId = 1;

        public MovimientosInventarioController(IMovimientoInventarioCommandService commandService, IMovimientoInventarioQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventarioResource>>> GetAll()
        {
            var movimientos = await _queryService.GetAllAsync();
            var resources = new List<MovimientoInventarioResource>();
            foreach (var movimiento in movimientos)
                resources.Add(MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventarioResource>> GetById(int id)
        {
            var query = new GetMovimientoInventarioByIdQuery(id, MockUserId);
            var movimiento = await _queryService.Handle(query);
            if (movimiento == null) return NotFound();
            return Ok(MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
        }

        [HttpPost]
        public async Task<ActionResult<MovimientoInventarioResource>> Create([FromBody] CreateMovimientoInventarioResource resource)
        {
            var command = CreateMovimientoInventarioCommandFromResourceAssembler.ToCommand(resource);
            var movimiento = await _commandService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = movimiento.Id }, MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovimientoInventarioResource>> Update(int id, [FromBody] CreateMovimientoInventarioResource resource)
        {
            var command = UpdateMovimientoInventarioCommandFromResourceAssembler.ToCommand(id, MockUserId, resource);
            var movimiento = await _commandService.Handle(command);
            if (movimiento == null) return NotFound();
            return Ok(MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = DeleteMovimientoInventarioCommandFromResourceAssembler.ToCommand(id, MockUserId);
            await _commandService.Handle(command);
            return NoContent();
        }

        // Endpoints originales mantenidos para compatibilidad
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MovimientoInventarioResource>>> GetAllByUserId(int userId)
        {
            var query = new GetAllMovimientosInventarioByUserIdQuery(userId);
            var movimientos = await _queryService.Handle(query);
            var resources = new List<MovimientoInventarioResource>();
            foreach (var movimiento in movimientos)
                resources.Add(MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
            return Ok(resources);
        }

        [HttpGet("{id}/user/{userId}")]
        public async Task<ActionResult<MovimientoInventarioResource>> GetByIdAndUserId(int id, int userId)
        {
            var query = new GetMovimientoInventarioByIdQuery(id, userId);
            var movimiento = await _queryService.Handle(query);
            if (movimiento == null) return NotFound();
            return Ok(MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<ActionResult<MovimientoInventarioResource>> UpdateWithUserId(int id, int userId, [FromBody] CreateMovimientoInventarioResource resource)
        {
            var command = UpdateMovimientoInventarioCommandFromResourceAssembler.ToCommand(id, userId, resource);
            var movimiento = await _commandService.Handle(command);
            if (movimiento == null) return NotFound();
            return Ok(MovimientoInventarioResourceFromEntityAssembler.ToResource(movimiento));
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> DeleteWithUserId(int id, int userId)
        {
            var command = DeleteMovimientoInventarioCommandFromResourceAssembler.ToCommand(id, userId);
            await _commandService.Handle(command);
            return NoContent();
        }
    }
} 