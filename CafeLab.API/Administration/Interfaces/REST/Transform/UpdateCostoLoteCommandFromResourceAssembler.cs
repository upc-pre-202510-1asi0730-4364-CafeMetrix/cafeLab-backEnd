using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Interfaces.REST.Resources;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class UpdateCostoLoteCommandFromResourceAssembler
    {
        public static UpdateCostoLoteCommand ToCommand(int id, int userId, CreateCostoLoteResource resource)
        {
            return new UpdateCostoLoteCommand(
                id,
                resource.Fecha,
                resource.Lote,
                resource.MateriaPrima,
                resource.ManoObra,
                resource.Transporte,
                resource.Almacenamiento,
                resource.Procesamiento,
                resource.OtrosCostos,
                userId
            );
        }
    }
} 