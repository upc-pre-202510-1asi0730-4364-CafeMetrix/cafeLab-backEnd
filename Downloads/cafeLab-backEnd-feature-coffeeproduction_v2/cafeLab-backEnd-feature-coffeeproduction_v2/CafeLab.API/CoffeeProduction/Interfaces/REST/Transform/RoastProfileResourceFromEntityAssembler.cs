using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

public class RoastProfileResourceFromEntityAssembler
{
    public RoastProfileResource ToResourceFromEntity(RoastProfile entity)
    {
        return new RoastProfileResource
        {
            Id = entity.Id,
            ProfileName = entity.ProfileName,
            RoastType = entity.RoastType,
            Duration = entity.Duration,
            CoffeeLotId = entity.CoffeeLotId,
            CoffeeLotName = entity.CoffeeLot?.LotName ?? string.Empty,
            TempStart = entity.TempStart,
            TempEnd = entity.TempEnd,
            IsFavorite = entity.IsFavorite,
            UserId = entity.UserId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
} 