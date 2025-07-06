using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Interfaces.REST.Resources;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class CreateMovimientoInventarioCommandFromResourceAssembler
    {
        public static CreateMovimientoInventarioCommand ToCommand(CreateMovimientoInventarioResource resource)
        {
            return new CreateMovimientoInventarioCommand(
                resource.Fecha,
                resource.Lote,
                resource.Producto,
                resource.Cantidad,
                resource.TipoCafe,
                resource.UserId
            );
        }
    }
} 