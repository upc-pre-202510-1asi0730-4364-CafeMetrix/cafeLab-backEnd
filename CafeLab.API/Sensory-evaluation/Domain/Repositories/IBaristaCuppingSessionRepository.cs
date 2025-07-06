using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;

namespace CafeLab.API.Sensory_evaluation.Domain.Repositories
{
    public interface IBaristaCuppingSessionRepository
    {
        Task<BaristaCuppingSession> AddAsync(BaristaCuppingSession session);
        Task<BaristaCuppingSession> UpdateAsync(BaristaCuppingSession session);
        Task DeleteAsync(int id, int userId);
        Task<BaristaCuppingSession> GetByIdAsync(int id, int userId);
        Task<IEnumerable<BaristaCuppingSession>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<BaristaCuppingSession>> GetAllAsync();
    }
} 