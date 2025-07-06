using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform
{
    public static class UpdateBaristaCuppingSessionCommandFromResourceAssembler
    {
        public static UpdateBaristaCuppingSessionCommand ToCommand(int id, int userId, CreateBaristaCuppingSessionResource resource)
        {
            return new UpdateBaristaCuppingSessionCommand(
                id,
                resource.Name,
                resource.Date,
                resource.Profile,
                userId
            );
        }
    }
} 