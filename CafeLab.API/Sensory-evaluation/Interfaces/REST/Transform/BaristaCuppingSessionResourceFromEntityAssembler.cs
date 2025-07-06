using CafeLab.API.Sensory_evaluation.Domain.Model.Aggregates;
using CafeLab.API.Sensory_evaluation.Interfaces.REST.Resources;
using System.Collections.Generic;
using System.Linq;

namespace CafeLab.API.Sensory_evaluation.Interfaces.REST.Transform
{
    public static class BaristaCuppingSessionResourceFromEntityAssembler
    {
        public static BaristaCuppingSessionResource ToResource(BaristaCuppingSession entity)
        {
            return new BaristaCuppingSessionResource
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Profile = entity.Profile,
                UserId = entity.UserId
            };
        }

        public static List<BaristaCuppingSessionResource> ToResourceList(IEnumerable<BaristaCuppingSession> entities)
        {
            return entities.Select(ToResource).ToList();
        }
    }
} 