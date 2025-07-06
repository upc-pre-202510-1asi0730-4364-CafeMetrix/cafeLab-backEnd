using CafeLab.API.Administration.Domain.Model.Commands;
using CafeLab.API.Administration.Interfaces.REST.Resources;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class CreateCostoLoteCommandFromResourceAssembler
    {
        public static CreateCostoLoteCommand ToCommand(CreateCostoLoteResource resource)
        {
            return new CreateCostoLoteCommand(
                resource.Fecha,
                resource.Lote,
                resource.MateriaPrima,
                resource.ManoObra,
                resource.Transporte,
                resource.Almacenamiento,
                resource.Procesamiento,
                resource.OtrosCostos,
                resource.UserId
            );
        }
    }
} 