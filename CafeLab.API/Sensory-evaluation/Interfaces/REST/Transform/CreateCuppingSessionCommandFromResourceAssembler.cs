using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Domain.Model.Commands;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform
{
    public static class CreateCuppingSessionCommandFromResourceAssembler
    {
        public static CreateCuppingSessionCommand ToCommand(CreateCuppingSessionResource resource)
        {
            return new CreateCuppingSessionCommand(
                resource.Name,
                resource.Date,
                resource.Origin,
                resource.Variety,
                resource.Process,
                resource.Lot,
                resource.Profile,
                new CuppingSessionRatings(
                    resource.Ratings.Fragancia,
                    resource.Ratings.Sabor,
                    resource.Ratings.Acidez,
                    resource.Ratings.Cuerpo,
                    resource.Ratings.Balance,
                    resource.Ratings.Postgusto
                ),
                resource.UserId
            );
        }
    }
} 