using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform
{
    public static class CuppingSessionResourceFromEntityAssembler
    {
        public static CuppingSessionResource ToResource(CuppingSession entity)
        {
            return new CuppingSessionResource
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Origin = entity.Origin,
                Variety = entity.Variety,
                Process = entity.Process,
                Lot = entity.Lot,
                Profile = entity.Profile,
                UserId = entity.UserId,
                Ratings = new RatingsResource
                {
                    Fragancia = entity.Ratings?.Fragancia ?? 0,
                    Sabor = entity.Ratings?.Sabor ?? 0,
                    Acidez = entity.Ratings?.Acidez ?? 0,
                    Cuerpo = entity.Ratings?.Cuerpo ?? 0,
                    Balance = entity.Ratings?.Balance ?? 0,
                    Postgusto = entity.Ratings?.Postgusto ?? 0
                }
            };
        }
    }
} 