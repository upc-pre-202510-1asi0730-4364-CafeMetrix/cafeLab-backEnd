using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CafeLab.API.Sensory_evaluation.Domain.Services;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform;
using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;
using CafeLab.API.Sensory_evaluation.Domain.Model.Queries;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/cuppingSessions")]
    public class CuppingSessionsController : ControllerBase
    {
        private readonly ICuppingSessionCommandService _commandService;
        private readonly ICuppingSessionQueryService _queryService;

        // Simulación de usuario autenticado - en producción esto vendría del token JWT
        private const int MockUserId = 1;

        public CuppingSessionsController(ICuppingSessionCommandService commandService, ICuppingSessionQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuppingSessionResource>>> GetAll()
        {
            var sessions = await _queryService.GetAllAsync();
            var resources = new List<CuppingSessionResource>();
            foreach (var session in sessions)
                resources.Add(CuppingSessionResourceFromEntityAssembler.ToResource(session));
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuppingSessionResource>> GetById(int id)
        {
            var query = new GetCuppingSessionByIdQuery(id, MockUserId);
            var session = await _queryService.Handle(query);
            if (session == null) return NotFound();
            return Ok(CuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpPost]
        public async Task<ActionResult<CuppingSessionResource>> Create([FromBody] CreateCuppingSessionResource resource)
        {
            var command = CreateCuppingSessionCommandFromResourceAssembler.ToCommand(resource);
            var session = await _commandService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = session.Id }, CuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CuppingSessionResource>> Update(int id, [FromBody] CreateCuppingSessionResource resource)
        {
            var command = new UpdateCuppingSessionCommand(
                id,
                resource.Name,
                resource.Date,
                resource.Origin,
                resource.Variety,
                resource.Process,
                resource.Lot,
                resource.Profile,
                new CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates.CuppingSessionRatings(
                    resource.Ratings.Fragancia,
                    resource.Ratings.Sabor,
                    resource.Ratings.Acidez,
                    resource.Ratings.Cuerpo,
                    resource.Ratings.Balance,
                    resource.Ratings.Postgusto
                ),
                MockUserId
            );
            var session = await _commandService.Handle(command);
            if (session == null) return NotFound();
            return Ok(CuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCuppingSessionCommand(id, MockUserId);
            await _commandService.Handle(command);
            return NoContent();
        }

        // Endpoints originales mantenidos para compatibilidad
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CuppingSessionResource>>> GetAllByUserId(int userId)
        {
            var query = new GetAllCuppingSessionsByUserIdQuery(userId);
            var sessions = await _queryService.Handle(query);
            var resources = new List<CuppingSessionResource>();
            foreach (var session in sessions)
                resources.Add(CuppingSessionResourceFromEntityAssembler.ToResource(session));
            return Ok(resources);
        }

        [HttpGet("{id}/user/{userId}")]
        public async Task<ActionResult<CuppingSessionResource>> GetByIdAndUserId(int id, int userId)
        {
            var query = new GetCuppingSessionByIdQuery(id, userId);
            var session = await _queryService.Handle(query);
            if (session == null) return NotFound();
            return Ok(CuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<ActionResult<CuppingSessionResource>> UpdateWithUserId(int id, int userId, [FromBody] CreateCuppingSessionResource resource)
        {
            var command = new UpdateCuppingSessionCommand(
                id,
                resource.Name,
                resource.Date,
                resource.Origin,
                resource.Variety,
                resource.Process,
                resource.Lot,
                resource.Profile,
                new CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates.CuppingSessionRatings(
                    resource.Ratings.Fragancia,
                    resource.Ratings.Sabor,
                    resource.Ratings.Acidez,
                    resource.Ratings.Cuerpo,
                    resource.Ratings.Balance,
                    resource.Ratings.Postgusto
                ),
                userId
            );
            var session = await _commandService.Handle(command);
            if (session == null) return NotFound();
            return Ok(CuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> DeleteWithUserId(int id, int userId)
        {
            var command = new DeleteCuppingSessionCommand(id, userId);
            await _commandService.Handle(command);
            return NoContent();
        }
    }
} 