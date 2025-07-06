using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Queries;
using CafeLab.API.Sensory_evaluation.Domain.Repositories;
using CafeLab.API.Sensory_evaluation.Domain.Services;

namespace CafeLab.API.Sensory_evaluation.Application.Internal.QueryServices
{
    public class CuppingSessionQueryService : ICuppingSessionQueryService
    {
        private readonly ICuppingSessionRepository _repository;
        public CuppingSessionQueryService(ICuppingSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CuppingSession> Handle(GetCuppingSessionByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id, query.UserId);
        }

        public async Task<IEnumerable<CuppingSession>> Handle(GetAllCuppingSessionsByUserIdQuery query)
        {
            return await _repository.GetAllByUserIdAsync(query.UserId);
        }

        public async Task<IEnumerable<CuppingSession>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
} 