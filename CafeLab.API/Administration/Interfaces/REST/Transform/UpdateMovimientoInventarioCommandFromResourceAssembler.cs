using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Interfaces.REST.Resources;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class UpdateMovimientoInventarioCommandFromResourceAssembler
    {
        public static UpdateMovimientoInventarioCommand ToCommand(int id, int userId, CreateMovimientoInventarioResource resource)
        {
            return new UpdateMovimientoInventarioCommand(
                id,
                resource.Fecha,
                resource.Lote,
                resource.Producto,
                resource.Cantidad,
                resource.TipoCafe,
                userId
            );
        }
    }
} 