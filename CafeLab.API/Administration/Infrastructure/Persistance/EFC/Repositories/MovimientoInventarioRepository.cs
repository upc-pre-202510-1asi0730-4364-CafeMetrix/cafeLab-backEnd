using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Administration.Infrastructure.Persistance.EFC.Repositories
{
    public class MovimientoInventarioRepository : IMovimientoInventarioRepository
    {
        private readonly AppDbContext _context;
        public MovimientoInventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MovimientoInventario> AddAsync(MovimientoInventario movimiento)
        {
            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();
            return movimiento;
        }

        public async Task<MovimientoInventario> UpdateAsync(MovimientoInventario movimiento)
        {
            _context.MovimientosInventario.Update(movimiento);
            await _context.SaveChangesAsync();
            return movimiento;
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var entity = await _context.MovimientosInventario.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (entity != null)
            {
                _context.MovimientosInventario.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MovimientoInventario> GetByIdAsync(int id, int userId)
        {
            return await _context.MovimientosInventario.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<IEnumerable<MovimientoInventario>> GetAllByUserIdAsync(int userId)
        {
            return await _context.MovimientosInventario.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<MovimientoInventario>> GetAllAsync()
        {
            return await _context.MovimientosInventario.ToListAsync();
        }
    }
} 