using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.CuppingSessions.Domain.Model;

namespace CafeLab.API.CuppingSessions.Application;

public interface ICuppingSessionApplicationService
{
    Task<CuppingSession?> GetByIdAsync(int id);
    Task<IEnumerable<CuppingSession>> GetAllAsync();
    Task AddAsync(CuppingSession cuppingSession);
    Task UpdateAsync(CuppingSession cuppingSession);
    Task DeleteAsync(int id);
} 