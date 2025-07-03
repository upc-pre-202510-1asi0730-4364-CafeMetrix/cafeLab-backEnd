namespace CafeLab.API.CoffeeProduction.Domain.Model.Commands;

using System.Collections.Generic;

public record CreateSupplierCommand(string Name, string Email, string Phone, string Location, List<string> Specialties, int UserId); 