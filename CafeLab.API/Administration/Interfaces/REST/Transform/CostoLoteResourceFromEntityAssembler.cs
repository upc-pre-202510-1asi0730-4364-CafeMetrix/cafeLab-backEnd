using CafeLab.API.Administration.Domain.Model.Aggregates;
using CafeLab.API.Administration.Interfaces.REST.Resources;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class CostoLoteResourceFromEntityAssembler
    {
        public static CostoLoteResource ToResource(CostoLote entity)
        {
            return new CostoLoteResource
            {
                Id = entity.Id,
                Fecha = entity.Fecha,
                Lote = entity.Lote,
                MateriaPrima = entity.MateriaPrima,
                ManoObra = entity.ManoObra,
                Transporte = entity.Transporte,
                Almacenamiento = entity.Almacenamiento,
                Procesamiento = entity.Procesamiento,
                OtrosCostos = entity.OtrosCostos,
                UserId = entity.UserId
            };
        }
    }
} 