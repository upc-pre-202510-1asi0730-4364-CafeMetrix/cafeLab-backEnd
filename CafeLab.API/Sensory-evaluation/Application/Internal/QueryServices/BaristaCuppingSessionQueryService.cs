using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Queries;
using CafeLab.API.Sensory_evaluation.Domain.Repositories;
using CafeLab.API.Sensory_evaluation.Domain.Services;

namespace CafeLab.API.Sensory_evaluation.Application.Internal.QueryServices
{
    public class BaristaCuppingSessionQueryService : IBaristaCuppingSessionQueryService
    {
        private readonly IBaristaCuppingSessionRepository _repository;
        public BaristaCuppingSessionQueryService(IBaristaCuppingSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaristaCuppingSession> Handle(GetBaristaCuppingSessionByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id, query.UserId);
        }

        public async Task<IEnumerable<BaristaCuppingSession>> Handle(GetAllBaristaCuppingSessionsByUserIdQuery query)
        {
            return await _repository.GetAllByUserIdAsync(query.UserId);
        }

        public async Task<IEnumerable<BaristaCuppingSession>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
} 