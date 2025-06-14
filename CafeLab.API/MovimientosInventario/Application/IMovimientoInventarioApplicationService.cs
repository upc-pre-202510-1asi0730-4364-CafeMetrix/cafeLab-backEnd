using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.MovimientosInventario.Domain.Model;

namespace CafeLab.API.MovimientosInventario.Application;

public interface IMovimientoInventarioApplicationService
{
    Task<MovimientoInventario?> GetByIdAsync(string id);
    Task<IEnumerable<MovimientoInventario>> GetAllAsync();
    Task AddAsync(MovimientoInventario movimientoInventario);
    Task UpdateAsync(MovimientoInventario movimientoInventario);
    Task DeleteAsync(string id);
} 