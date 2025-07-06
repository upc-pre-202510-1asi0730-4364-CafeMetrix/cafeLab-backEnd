using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Sensory_evaluation.Infrastructure.Persistance.EFC.Repositories
{
    public class CuppingSessionRepository : ICuppingSessionRepository
    {
        private readonly AppDbContext _context;
        public CuppingSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CuppingSession> AddAsync(CuppingSession cuppingSession)
        {
            _context.CuppingSessions.Add(cuppingSession);
            await _context.SaveChangesAsync();
            return cuppingSession;
        }

        public async Task<CuppingSession> UpdateAsync(CuppingSession cuppingSession)
        {
            _context.CuppingSessions.Update(cuppingSession);
            await _context.SaveChangesAsync();
            return cuppingSession;
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var entity = await _context.CuppingSessions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (entity != null)
            {
                _context.CuppingSessions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CuppingSession> GetByIdAsync(int id, int userId)
        {
            return await _context.CuppingSessions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<IEnumerable<CuppingSession>> GetAllByUserIdAsync(int userId)
        {
            return await _context.CuppingSessions.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<CuppingSession>> GetAllAsync()
        {
            return await _context.CuppingSessions.ToListAsync();
        }
    }
} 