using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

public class CreateCoffeeLotCommandFromResourceAssembler
{
    public CreateCoffeeLotCommand ToCommandFromResource(CreateCoffeeLotResource resource)
    {
        return new CreateCoffeeLotCommand
        {
            LotName = resource.LotName,
            CoffeeType = resource.CoffeeType,
            ProcessingMethod = resource.ProcessingMethod,
            Altitude = resource.Altitude,
            Weight = resource.Weight,
            Certifications = resource.Certifications,
            Origin = resource.Origin,
            SupplierId = resource.SupplierId,
            UserId = resource.UserId,
            Status = resource.Status
        };
    }
} 