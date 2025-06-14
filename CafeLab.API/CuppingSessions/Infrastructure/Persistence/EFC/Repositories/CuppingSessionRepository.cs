using CafeLab.API.CuppingSessions.Domain.Model;
using CafeLab.API.CuppingSessions.Domain.Repositories;
using CafeLab.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.CuppingSessions.Infrastructure.Persistence.EFC.Repositories;

public class CuppingSessionRepository : ICuppingSessionRepository
{
    private readonly ApplicationDbContext _context;

    public CuppingSessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CuppingSession?> GetByIdAsync(int id)
    {
        return await _context.CuppingSessions.FindAsync(id);
    }

    public async Task<IEnumerable<CuppingSession>> GetAllAsync()
    {
        return await _context.CuppingSessions.ToListAsync();
    }

    public async Task AddAsync(CuppingSession cuppingSession)
    {
        await _context.CuppingSessions.AddAsync(cuppingSession);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CuppingSession cuppingSession)
    {
        _context.CuppingSessions.Update(cuppingSession);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cuppingSession = await GetByIdAsync(id);
        if (cuppingSession != null)
        {
            _context.CuppingSessions.Remove(cuppingSession);
            await _context.SaveChangesAsync();
        }
    }
} 