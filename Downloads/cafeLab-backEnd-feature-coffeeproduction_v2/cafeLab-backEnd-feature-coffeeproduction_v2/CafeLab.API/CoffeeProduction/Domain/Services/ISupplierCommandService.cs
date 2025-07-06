using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Commands;

namespace CafeLab.API.CoffeeProduction.Domain.Services;

public interface ISupplierCommandService
{
    Task<Supplier?> Handle(CreateSupplierCommand command);
    Task<Supplier?> Handle(UpdateSupplierCommand command);
    Task<bool> Handle(DeleteSupplierCommand command);
} 