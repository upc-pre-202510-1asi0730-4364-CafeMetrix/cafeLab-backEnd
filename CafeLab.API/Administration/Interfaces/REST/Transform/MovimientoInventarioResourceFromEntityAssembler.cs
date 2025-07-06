using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Interfaces.REST.Resources;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class MovimientoInventarioResourceFromEntityAssembler
    {
        public static MovimientoInventarioResource ToResource(MovimientoInventario entity)
        {
            return new MovimientoInventarioResource
            {
                Id = entity.Id,
                Fecha = entity.Fecha,
                Lote = entity.Lote,
                Producto = entity.Producto,
                Cantidad = entity.Cantidad,
                TipoCafe = entity.TipoCafe,
                UserId = entity.UserId
            };
        }
    }
} 