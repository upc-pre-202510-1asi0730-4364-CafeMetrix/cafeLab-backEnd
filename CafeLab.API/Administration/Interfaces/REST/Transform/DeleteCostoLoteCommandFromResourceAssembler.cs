using CafeLab.API.Administration.Domain.Model.Commands;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class DeleteCostoLoteCommandFromResourceAssembler
    {
        public static DeleteCostoLoteCommand ToCommand(int id, int userId)
        {
            return new DeleteCostoLoteCommand(id, userId);
        }
    }
} 