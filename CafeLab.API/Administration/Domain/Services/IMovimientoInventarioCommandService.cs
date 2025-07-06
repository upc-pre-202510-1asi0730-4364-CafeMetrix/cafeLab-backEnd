using System.Threading.Tasks;
using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Domain.Model.Commands;

namespace CafeLab.API.Administration.Domain.Services
{
    public interface IMovimientoInventarioCommandService
    {
        Task<MovimientoInventario> Handle(CreateMovimientoInventarioCommand command);
        Task<MovimientoInventario> Handle(UpdateMovimientoInventarioCommand command);
        Task Handle(DeleteMovimientoInventarioCommand command);
    }
} 