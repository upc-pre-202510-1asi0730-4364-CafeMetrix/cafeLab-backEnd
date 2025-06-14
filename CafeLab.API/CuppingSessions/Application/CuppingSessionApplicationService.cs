using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.CuppingSessions.Domain.Model;
using CafeLab.API.CuppingSessions.Domain.Repositories;

namespace CafeLab.API.CuppingSessions.Application;

public class CuppingSessionApplicationService : ICuppingSessionApplicationService
{
    private readonly ICuppingSessionRepository _cuppingSessionRepository;

    public CuppingSessionApplicationService(ICuppingSessionRepository cuppingSessionRepository)
    {
        _cuppingSessionRepository = cuppingSessionRepository;
    }

    public async Task<CuppingSession?> GetByIdAsync(int id)
    {
        return await _cuppingSessionRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<CuppingSession>> GetAllAsync()
    {
        return await _cuppingSessionRepository.GetAllAsync();
    }

    public async Task AddAsync(CuppingSession cuppingSession)
    {
        await _cuppingSessionRepository.AddAsync(cuppingSession);
    }

    public async Task UpdateAsync(CuppingSession cuppingSession)
    {
        await _cuppingSessionRepository.UpdateAsync(cuppingSession);
    }

    public async Task DeleteAsync(int id)
    {
        await _cuppingSessionRepository.DeleteAsync(id);
    }
} 