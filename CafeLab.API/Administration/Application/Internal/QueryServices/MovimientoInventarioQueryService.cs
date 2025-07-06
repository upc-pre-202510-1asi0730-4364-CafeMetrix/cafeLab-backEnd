using System.Collections.Generic;
using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Queries;
using CafeLab.API.Administration.Domain.Repositories;
using CafeLab.API.Administration.Domain.Services;

namespace CafeLab.API.Administration.Application.Internal.QueryServices
{
    public class MovimientoInventarioQueryService : IMovimientoInventarioQueryService
    {
        private readonly IMovimientoInventarioRepository _repository;
        public MovimientoInventarioQueryService(IMovimientoInventarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovimientoInventario> Handle(GetMovimientoInventarioByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id, query.UserId);
        }

        public async Task<IEnumerable<MovimientoInventario>> Handle(GetAllMovimientosInventarioByUserIdQuery query)
        {
            return await _repository.GetAllByUserIdAsync(query.UserId);
        }

        public async Task<IEnumerable<MovimientoInventario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
} 