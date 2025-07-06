using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;

namespace CafeLab.API.Administration.Domain.Repositories
{
    public interface ICostoLoteRepository
    {
        Task<CostoLote> AddAsync(CostoLote costoLote);
        Task<CostoLote> UpdateAsync(CostoLote costoLote);
        Task DeleteAsync(int id, int userId);
        Task<CostoLote> GetByIdAsync(int id, int userId);
        Task<IEnumerable<CostoLote>> GetAllByUserIdAsync(int userId);
    }
} 