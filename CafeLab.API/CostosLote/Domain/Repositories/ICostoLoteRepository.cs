using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.CostosLote.Domain.Model;

namespace CafeLab.API.CostosLote.Domain.Repositories;

public interface ICostoLoteRepository
{
    Task<CostoLote?> GetByIdAsync(int id);
    Task<IEnumerable<CostoLote>> GetAllAsync();
    Task AddAsync(CostoLote costoLote);
    Task UpdateAsync(CostoLote costoLote);
    Task DeleteAsync(int id);
} 