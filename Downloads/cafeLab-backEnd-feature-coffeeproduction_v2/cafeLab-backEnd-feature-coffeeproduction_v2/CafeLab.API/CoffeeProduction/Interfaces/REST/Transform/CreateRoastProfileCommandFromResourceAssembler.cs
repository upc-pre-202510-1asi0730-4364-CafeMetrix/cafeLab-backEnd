using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

public class CreateRoastProfileCommandFromResourceAssembler
{
    public CreateRoastProfileCommand ToCommandFromResource(CreateRoastProfileResource resource)
    {
        return new CreateRoastProfileCommand
        {
            ProfileName = resource.ProfileName,
            RoastType = resource.RoastType,
            Duration = resource.Duration,
            CoffeeLotId = resource.CoffeeLotId,
            TempStart = resource.TempStart,
            TempEnd = resource.TempEnd,
            IsFavorite = resource.IsFavorite,
            UserId = resource.UserId
        };
    }
} 