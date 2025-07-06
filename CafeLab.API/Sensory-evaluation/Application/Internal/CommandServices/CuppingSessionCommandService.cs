using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;
using CafeLab.API.Sensory_evaluation.Domain.Repositories;
using CafeLab.API.Sensory_evaluation.Domain.Services;

namespace CafeLab.API.Sensory_evaluation.Application.Internal.CommandServices
{
    public class CuppingSessionCommandService : ICuppingSessionCommandService
    {
        private readonly ICuppingSessionRepository _repository;
        public CuppingSessionCommandService(ICuppingSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CuppingSession> Handle(CreateCuppingSessionCommand command)
        {
            var cuppingSession = new CuppingSession(
                command.Name,
                command.Date,
                command.Origin,
                command.Variety,
                command.Process,
                command.Lot,
                command.Profile,
                command.Ratings,
                command.UserId
            );
            return await _repository.AddAsync(cuppingSession);
        }

        public async Task<CuppingSession> Handle(UpdateCuppingSessionCommand command)
        {
            var cuppingSession = await _repository.GetByIdAsync(command.Id, command.UserId);
            if (cuppingSession == null) return null;
            cuppingSession.Update(
                command.Name,
                command.Date,
                command.Origin,
                command.Variety,
                command.Process,
                command.Lot,
                command.Profile,
                command.Ratings
            );
            return await _repository.UpdateAsync(cuppingSession);
        }

        public async Task Handle(DeleteCuppingSessionCommand command)
        {
            await _repository.DeleteAsync(command.Id, command.UserId);
        }
    }
} 