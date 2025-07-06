using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;

namespace CafeLab.API.Sensory_evaluation.Domain.Repositories
{
    public interface ICuppingSessionRepository
    {
        Task<CuppingSession> AddAsync(CuppingSession cuppingSession);
        Task<CuppingSession> UpdateAsync(CuppingSession cuppingSession);
        Task DeleteAsync(int id, int userId);
        Task<CuppingSession> GetByIdAsync(int id, int userId);
        Task<IEnumerable<CuppingSession>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<CuppingSession>> GetAllAsync();
    }
} 