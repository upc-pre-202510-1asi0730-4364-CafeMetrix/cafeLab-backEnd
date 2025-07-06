using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Queries;
using CafeLab.API.Administration.Domain.Repositories;
using CafeLab.API.Administration.Domain.Services;

namespace CafeLab.API.Administration.Application.Internal.QueryServices
{
    public class CostoLoteQueryService : ICostoLoteQueryService
    {
        private readonly ICostoLoteRepository _repository;
        public CostoLoteQueryService(ICostoLoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<CostoLote> Handle(GetCostoLoteByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id, query.UserId);
        }

        public async Task<IEnumerable<CostoLote>> Handle(GetAllCostosLoteByUserIdQuery query)
        {
            return await _repository.GetAllByUserIdAsync(query.UserId);
        }
    }
} 