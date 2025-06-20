using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.CostosLote.Domain.Model;
using CafeLab.API.CostosLote.Domain.Repositories;

namespace CafeLab.API.CostosLote.Application.Internal.CommandServices;

public class CostoLoteApplicationService : ICostoLoteApplicationService
{
    private readonly ICostoLoteRepository _costoLoteRepository;

    public CostoLoteApplicationService(ICostoLoteRepository costoLoteRepository)
    {
        _costoLoteRepository = costoLoteRepository;
    }

    public async Task<CostoLote?> GetByIdAsync(int id)
    {
        return await _costoLoteRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<CostoLote>> GetAllAsync()
    {
        return await _costoLoteRepository.GetAllAsync();
    }

    public async Task AddAsync(CostoLote costoLote)
    {
        await _costoLoteRepository.AddAsync(costoLote);
    }

    public async Task UpdateAsync(CostoLote costoLote)
    {
        await _costoLoteRepository.UpdateAsync(costoLote);
    }

    public async Task DeleteAsync(int id)
    {
        await _costoLoteRepository.DeleteAsync(id);
    }
} 