using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform
{
    public static class DeleteBaristaCuppingSessionCommandFromResourceAssembler
    {
        public static DeleteBaristaCuppingSessionCommand ToCommand(int id, int userId)
        {
            return new DeleteBaristaCuppingSessionCommand(id, userId);
        }
    }
} 