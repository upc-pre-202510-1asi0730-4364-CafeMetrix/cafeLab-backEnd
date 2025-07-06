namespace CafeLab.API.CoffeeProduction.Domain.Model.Commands;

using System.Collections.Generic;

public record UpdateSupplierCommand(int Id, string Name, string Email, string Phone, string Location, List<string> Specialties, int UserId); 