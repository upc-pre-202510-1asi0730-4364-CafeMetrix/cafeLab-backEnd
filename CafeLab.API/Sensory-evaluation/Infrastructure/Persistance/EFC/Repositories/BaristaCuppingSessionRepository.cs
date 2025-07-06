using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Sensory_evaluation.Infrastructure.Persistance.EFC.Repositories
{
    public class BaristaCuppingSessionRepository : IBaristaCuppingSessionRepository
    {
        private readonly AppDbContext _context;
        public BaristaCuppingSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BaristaCuppingSession> AddAsync(BaristaCuppingSession session)
        {
            _context.BaristaCuppingSessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<BaristaCuppingSession> UpdateAsync(BaristaCuppingSession session)
        {
            _context.BaristaCuppingSessions.Update(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var entity = await _context.BaristaCuppingSessions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (entity != null)
            {
                _context.BaristaCuppingSessions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BaristaCuppingSession> GetByIdAsync(int id, int userId)
        {
            return await _context.BaristaCuppingSessions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<IEnumerable<BaristaCuppingSession>> GetAllByUserIdAsync(int userId)
        {
            return await _context.BaristaCuppingSessions.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<BaristaCuppingSession>> GetAllAsync()
        {
            return await _context.BaristaCuppingSessions.ToListAsync();
        }
    }
} 