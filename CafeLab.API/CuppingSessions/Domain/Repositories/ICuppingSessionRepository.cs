using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.CuppingSessions.Domain.Model;

namespace CafeLab.API.CuppingSessions.Domain.Repositories;

public interface ICuppingSessionRepository
{
    Task<CuppingSession?> GetByIdAsync(int id);
    Task<IEnumerable<CuppingSession>> GetAllAsync();
    Task AddAsync(CuppingSession cuppingSession);
    Task UpdateAsync(CuppingSession cuppingSession);
    Task DeleteAsync(int id);
} 