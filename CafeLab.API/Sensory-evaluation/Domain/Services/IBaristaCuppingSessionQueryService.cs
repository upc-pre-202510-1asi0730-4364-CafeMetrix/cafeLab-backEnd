using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Queries;

namespace CafeLab.API.Sensory_evaluation.Domain.Services
{
    public interface IBaristaCuppingSessionQueryService
    {
        Task<BaristaCuppingSession> Handle(GetBaristaCuppingSessionByIdQuery query);
        Task<IEnumerable<BaristaCuppingSession>> Handle(GetAllBaristaCuppingSessionsByUserIdQuery query);
        Task<IEnumerable<BaristaCuppingSession>> GetAllAsync();
    }
} 