using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Queries;

namespace CafeLab.API.Sensory_evaluation.Domain.Services
{
    public interface ICuppingSessionQueryService
    {
        Task<CuppingSession> Handle(GetCuppingSessionByIdQuery query);
        Task<IEnumerable<CuppingSession>> Handle(GetAllCuppingSessionsByUserIdQuery query);
        Task<IEnumerable<CuppingSession>> GetAllAsync();
    }
} 