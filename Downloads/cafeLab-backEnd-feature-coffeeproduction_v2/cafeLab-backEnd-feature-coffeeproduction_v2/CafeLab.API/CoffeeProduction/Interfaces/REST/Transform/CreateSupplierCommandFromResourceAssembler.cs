using CafeLab.API.CoffeeProduction.Domain.Model.Commands;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

public static class CreateSupplierCommandFromResourceAssembler
{
    public static CreateSupplierCommand ToCommandFromResource(CreateSupplierResource resource)
    {
        return new CreateSupplierCommand(
            resource.Name,
            resource.Email,
            resource.Phone,
            resource.Location,
            resource.Specialties,
            resource.UserId
        );
    }
} 