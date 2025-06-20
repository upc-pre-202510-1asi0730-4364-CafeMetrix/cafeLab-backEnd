using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.MovimientosInventario.Domain.Model;
using CafeLab.API.MovimientosInventario.Domain.Repositories;

namespace CafeLab.API.MovimientosInventario.Application;

public class MovimientoInventarioApplicationService : IMovimientoInventarioApplicationService
{
    private readonly IMovimientoInventarioRepository _movimientoInventarioRepository;

    public MovimientoInventarioApplicationService(IMovimientoInventarioRepository movimientoInventarioRepository)
    {
        _movimientoInventarioRepository = movimientoInventarioRepository;
    }

    public async Task<MovimientoInventario?> GetByIdAsync(string id)
    {
        return await _movimientoInventarioRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<MovimientoInventario>> GetAllAsync()
    {
        return await _movimientoInventarioRepository.GetAllAsync();
    }

    public async Task AddAsync(MovimientoInventario movimientoInventario)
    {
        await _movimientoInventarioRepository.AddAsync(movimientoInventario);
    }

    public async Task UpdateAsync(MovimientoInventario movimientoInventario)
    {
        await _movimientoInventarioRepository.UpdateAsync(movimientoInventario);
    }

    public async Task DeleteAsync(string id)
    {
        await _movimientoInventarioRepository.DeleteAsync(id);
    }
} 