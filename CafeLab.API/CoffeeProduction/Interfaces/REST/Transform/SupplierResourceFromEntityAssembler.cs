using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier entity)
    {
        return new SupplierResource(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Phone,
            entity.Location,
            entity.Specialties,
            entity.UserId
        );
    }
} 