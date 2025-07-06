using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Administration.Infrastructure.Persistance.EFC.Repositories
{
    public class CostoLoteRepository : ICostoLoteRepository
    {
        private readonly AppDbContext _context;
        public CostoLoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CostoLote> AddAsync(CostoLote costoLote)
        {
            _context.CostosLote.Add(costoLote);
            await _context.SaveChangesAsync();
            return costoLote;
        }

        public async Task<CostoLote> UpdateAsync(CostoLote costoLote)
        {
            _context.CostosLote.Update(costoLote);
            await _context.SaveChangesAsync();
            return costoLote;
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var entity = await _context.CostosLote.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (entity != null)
            {
                _context.CostosLote.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CostoLote> GetByIdAsync(int id, int userId)
        {
            return await _context.CostosLote.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<IEnumerable<CostoLote>> GetAllByUserIdAsync(int userId)
        {
            return await _context.CostosLote.Where(x => x.UserId == userId).ToListAsync();
        }
    }
} 