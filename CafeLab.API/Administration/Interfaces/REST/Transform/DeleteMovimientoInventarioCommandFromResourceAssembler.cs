using CafeLab.API.Administration.Domain.Model.Commands;

namespace CafeLab.API.Administration.Interfaces.REST.Transform
{
    public static class DeleteMovimientoInventarioCommandFromResourceAssembler
    {
        public static DeleteMovimientoInventarioCommand ToCommand(int id, int userId)
        {
            return new DeleteMovimientoInventarioCommand(id, userId);
        }
    }
} 