using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Domain.Repositories;
using CafeLab.API.Administration.Domain.Services;

namespace CafeLab.API.Administration.Application.Internal.CommandServices
{
    public class MovimientoInventarioCommandService : IMovimientoInventarioCommandService
    {
        private readonly IMovimientoInventarioRepository _repository;
        public MovimientoInventarioCommandService(IMovimientoInventarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovimientoInventario> Handle(CreateMovimientoInventarioCommand command)
        {
            var movimiento = new MovimientoInventario(
                command.Fecha,
                command.Lote,
                command.Producto,
                command.Cantidad,
                command.TipoCafe,
                command.UserId
            );
            return await _repository.AddAsync(movimiento);
        }

        public async Task<MovimientoInventario> Handle(UpdateMovimientoInventarioCommand command)
        {
            var movimiento = await _repository.GetByIdAsync(command.Id, command.UserId);
            if (movimiento == null) return null;
            movimiento.Update(
                command.Fecha,
                command.Lote,
                command.Producto,
                command.Cantidad,
                command.TipoCafe
            );
            return await _repository.UpdateAsync(movimiento);
        }

        public async Task Handle(DeleteMovimientoInventarioCommand command)
        {
            await _repository.DeleteAsync(command.Id, command.UserId);
        }
    }
} 