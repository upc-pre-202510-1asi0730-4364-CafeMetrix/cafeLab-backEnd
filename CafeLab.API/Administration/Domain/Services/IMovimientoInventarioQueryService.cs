using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Queries;

namespace CafeLab.API.Administration.Domain.Services
{
    public interface IMovimientoInventarioQueryService
    {
        Task<MovimientoInventario> Handle(GetMovimientoInventarioByIdQuery query);
        Task<IEnumerable<MovimientoInventario>> Handle(GetAllMovimientosInventarioByUserIdQuery query);
        Task<IEnumerable<MovimientoInventario>> GetAllAsync();
    }
} 