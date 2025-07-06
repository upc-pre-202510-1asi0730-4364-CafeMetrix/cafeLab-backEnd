namespace CafeLab.API.CoffeeProduction.Interfaces.REST.Resources;

using System.Collections.Generic;

public record CreateSupplierResource(string Name, string Email, string Phone, string Location, List<string> Specialties, int UserId); 