using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Queries;

namespace CafeLab.API.Administration.Domain.Services
{
    public interface ICostoLoteQueryService
    {
        Task<CostoLote> Handle(GetCostoLoteByIdQuery query);
        Task<IEnumerable<CostoLote>> Handle(GetAllCostosLoteByUserIdQuery query);
    }
} 