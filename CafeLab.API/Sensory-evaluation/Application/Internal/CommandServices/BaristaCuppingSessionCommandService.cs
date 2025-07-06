using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;
using CafeLab.API.Sensory_evaluation.Domain.Repositories;
using CafeLab.API.Sensory_evaluation.Domain.Services;

namespace CafeLab.API.Sensory_evaluation.Application.Internal.CommandServices
{
    public class BaristaCuppingSessionCommandService : IBaristaCuppingSessionCommandService
    {
        private readonly IBaristaCuppingSessionRepository _repository;
        public BaristaCuppingSessionCommandService(IBaristaCuppingSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaristaCuppingSession> Handle(CreateBaristaCuppingSessionCommand command)
        {
            var session = new BaristaCuppingSession(
                command.Name,
                command.Date,
                command.Profile,
                command.UserId
            );
            return await _repository.AddAsync(session);
        }

        public async Task<BaristaCuppingSession> Handle(UpdateBaristaCuppingSessionCommand command)
        {
            var session = await _repository.GetByIdAsync(command.Id, command.UserId);
            if (session == null) return null;
            session.Update(
                command.Name,
                command.Date,
                command.Profile
            );
            return await _repository.UpdateAsync(session);
        }

        public async Task Handle(DeleteBaristaCuppingSessionCommand command)
        {
            await _repository.DeleteAsync(command.Id, command.UserId);
        }
    }
} 