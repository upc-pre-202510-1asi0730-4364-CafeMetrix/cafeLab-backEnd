using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;

namespace CafeLab.API.Administration.Domain.Repositories
{
    public interface IMovimientoInventarioRepository
    {
        Task<MovimientoInventario> AddAsync(MovimientoInventario movimiento);
        Task<MovimientoInventario> UpdateAsync(MovimientoInventario movimiento);
        Task DeleteAsync(int id, int userId);
        Task<MovimientoInventario> GetByIdAsync(int id, int userId);
        Task<IEnumerable<MovimientoInventario>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<MovimientoInventario>> GetAllAsync();
    }
} 