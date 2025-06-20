using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CafeLab.API.CostosLote.Domain.Model;
using CafeLab.API.CostosLote.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace CafeLab.API.CostosLote.Infrastructure.Persistence.EFC.Repositories;

public class CostoLoteRepository : ICostoLoteRepository
{
    private readonly AppDbContext _context;

    public CostoLoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CostoLote?> GetByIdAsync(int id)
    {
        return await _context.CostosLote.FindAsync(id);
    }

    public async Task<IEnumerable<CostoLote>> GetAllAsync()
    {
        return await _context.CostosLote.ToListAsync();
    }

    public async Task AddAsync(CostoLote costoLote)
    {
        await _context.CostosLote.AddAsync(costoLote);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CostoLote costoLote)
    {
        _context.CostosLote.Update(costoLote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var costoLote = await GetByIdAsync(id);
        if (costoLote != null)
        {
            _context.CostosLote.Remove(costoLote);
            await _context.SaveChangesAsync();
        }
    }
} 