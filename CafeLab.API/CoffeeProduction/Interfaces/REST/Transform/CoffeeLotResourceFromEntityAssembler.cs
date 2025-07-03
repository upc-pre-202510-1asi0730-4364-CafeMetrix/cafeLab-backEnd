using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;
using System.Text.Json;

namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Transform;

public class CoffeeLotResourceFromEntityAssembler
{
    public CoffeeLotResource ToResourceFromEntity(CoffeeLot entity)
    {
        var certifications = new List<string>();
        if (!string.IsNullOrEmpty(entity.Certifications))
        {
            try
            {
                certifications = JsonSerializer.Deserialize<List<string>>(entity.Certifications) ?? new List<string>();
            }
            catch
            {
                certifications = new List<string>();
            }
        }

        return new CoffeeLotResource
        {
            Id = entity.Id,
            LotName = entity.LotName,
            CoffeeType = entity.CoffeeType,
            ProcessingMethod = entity.ProcessingMethod,
            Altitude = entity.Altitude,
            Weight = entity.Weight,
            Certifications = certifications,
            Origin = entity.Origin,
            SupplierId = entity.SupplierId,
            SupplierName = entity.Supplier?.Name ?? string.Empty,
            UserId = entity.UserId,
            Status = entity.Status
        };
    }
} 