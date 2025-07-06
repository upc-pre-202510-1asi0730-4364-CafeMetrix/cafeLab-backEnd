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
    [Route("api/v1/barista-cupping-sessions")]
    public class BaristaCuppingSessionsController : ControllerBase
    {
        private readonly IBaristaCuppingSessionCommandService _commandService;
        private readonly IBaristaCuppingSessionQueryService _queryService;

        public BaristaCuppingSessionsController(IBaristaCuppingSessionCommandService commandService, IBaristaCuppingSessionQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BaristaCuppingSessionResource>>> GetAllByUserId(int userId)
        {
            var query = new GetAllBaristaCuppingSessionsByUserIdQuery(userId);
            var sessions = await _queryService.Handle(query);
            var resources = new List<BaristaCuppingSessionResource>();
            foreach (var session in sessions)
                resources.Add(BaristaCuppingSessionResourceFromEntityAssembler.ToResource(session));
            return Ok(resources);
        }

        [HttpGet("{id}/user/{userId}")]
        public async Task<ActionResult<BaristaCuppingSessionResource>> GetById(int id, int userId)
        {
            var query = new GetBaristaCuppingSessionByIdQuery(id, userId);
            var session = await _queryService.Handle(query);
            if (session == null) return NotFound();
            return Ok(BaristaCuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpPost]
        public async Task<ActionResult<BaristaCuppingSessionResource>> Create([FromBody] CreateBaristaCuppingSessionResource resource)
        {
            var command = CreateBaristaCuppingSessionCommandFromResourceAssembler.ToCommand(resource);
            var session = await _commandService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = session.Id, userId = session.UserId }, BaristaCuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<ActionResult<BaristaCuppingSessionResource>> Update(int id, int userId, [FromBody] CreateBaristaCuppingSessionResource resource)
        {
            var command = UpdateBaristaCuppingSessionCommandFromResourceAssembler.ToCommand(id, userId, resource);
            var session = await _commandService.Handle(command);
            if (session == null) return NotFound();
            return Ok(BaristaCuppingSessionResourceFromEntityAssembler.ToResource(session));
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, int userId)
        {
            var command = DeleteBaristaCuppingSessionCommandFromResourceAssembler.ToCommand(id, userId);
            await _commandService.Handle(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaristaCuppingSessionResource>>> GetAll()
        {
            var sessions = await _queryService.GetAllAsync();
            var resources = BaristaCuppingSessionResourceFromEntityAssembler.ToResourceList(sessions);
            return Ok(resources);
        }
    }
} 