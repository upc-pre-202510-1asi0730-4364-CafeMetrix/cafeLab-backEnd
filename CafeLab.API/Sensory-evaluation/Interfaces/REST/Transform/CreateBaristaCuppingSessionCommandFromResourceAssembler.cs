using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform
{
    public static class CreateBaristaCuppingSessionCommandFromResourceAssembler
    {
        public static CreateBaristaCuppingSessionCommand ToCommand(CreateBaristaCuppingSessionResource resource)
        {
            return new CreateBaristaCuppingSessionCommand(
                resource.Name,
                resource.Date,
                resource.Profile,
                resource.UserId
            );
        }
    }
} 