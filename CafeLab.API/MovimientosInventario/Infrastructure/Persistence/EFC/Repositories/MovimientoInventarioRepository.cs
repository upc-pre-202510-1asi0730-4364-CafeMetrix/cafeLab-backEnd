using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CafeLab.API.MovimientosInventario.Domain.Model;
using CafeLab.API.MovimientosInventario.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace CafeLab.API.MovimientosInventario.Infrastructure.Persistence.EFC.Repositories;

public class MovimientoInventarioRepository : IMovimientoInventarioRepository
{
    private readonly AppDbContext _context;

    public MovimientoInventarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MovimientoInventario?> GetByIdAsync(string id)
    {
        return await _context.MovimientosInventario.FindAsync(id);
    }

    public async Task<IEnumerable<MovimientoInventario>> GetAllAsync()
    {
        return await _context.MovimientosInventario.ToListAsync();
    }

    public async Task AddAsync(MovimientoInventario movimientoInventario)
    {
        await _context.MovimientosInventario.AddAsync(movimientoInventario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MovimientoInventario movimientoInventario)
    {
        _context.MovimientosInventario.Update(movimientoInventario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var movimientoInventario = await GetByIdAsync(id);
        if (movimientoInventario != null)
        {
            _context.MovimientosInventario.Remove(movimientoInventario);
            await _context.SaveChangesAsync();
        }
    }
} 